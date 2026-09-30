<template>
  <v-layout>
    <LoginAuth v-if="!isLoggeInd"/>
    <v-app v-else id="app" class="w-100 h-100">
      <AppBar @toggle-drawer="toggleDrawer" @nameRoute="nameTab" @logout="clearApp"/>
      <v-navigation-drawer id="drawer" v-model="data.drawer" class="position-fixed">
        <v-list density="compact" v-model:selected="data.selectedItems" :mandatory="true" select-strategy="leaf">
            <v-list-subheader><small class="font-weight-bold">DASHBOARD</small></v-list-subheader>
            <v-list-item prepend-icon="mdi-home" color="red-darken-4" title="Inicio" :lines="true" rounded class="mx-2" value="Inicio" @click="clearApp()"/>
            <v-list-subheader><small class="font-weight-bold">MOVIMIENTOS</small></v-list-subheader>
            <v-list-group prepend-icon="mdi-cash-register">
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" rounded value="Venta" :lines="true" color="red-darken-4" class="mx-2" title="Ventas"/>
                </template>
                <v-list-item class="mx-2" rounded :lines="true" color="red-darken-4" v-for="([title, route, icon], i) in data.ventasActions" 
                    :key="i" :value="title" @click="nameTab(route)">
                    <small>- {{ title }}</small>
                </v-list-item>
            </v-list-group>
            <v-list-group prepend-icon="mdi-cart-arrow-down">
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" rounded value="Compras  " :lines="true" color="red-darken-4" class="mx-2" title="Compras"/>
                </template>
                <v-list-item class="mx-2" rounded :lines="true" color="red-darken-4" v-for="([title, route, icon], i) in data.compraActions"
                    :key="i" :value="title" @click="nameTab(route)">
                    <small>- {{ title }}</small>
                </v-list-item>
            </v-list-group>
            <v-list-subheader><small class="font-weight-bold">GESTIÓN GENERAL</small></v-list-subheader>
            <v-list-group prepend-icon="mdi-package-variant">
                <template v-slot:activator="{ props }">
                    <v-list-item v-bind="props" rounded value="Inventario" :lines="true" color="red-darken-4" class="mx-2" title="Inventario"/>
                </template>
                <v-list-item class="mx-2" rounded :lines="true" color="red-darken-4" v-for="([title, route, icon], i) in data.managerStock" 
                    :key="i" :value="title" @click="nameTab(route)">
                    <small>- {{ title }}</small>
                </v-list-item>
            </v-list-group>
          <v-list-group prepend-icon="mdi-account-group">
            <template v-slot:activator="{ props }">
              <v-list-item v-bind="props" rounded value="Inventario" :lines="true" color="red-darken-4" class="mx-2" title="Clientes"/>
            </template>
            <v-list-item class="mx-2" rounded :lines="true" color="red-darken-4" v-for="([title, icon, route], i) in data.clientes"
              :key="i" :value="title" @click="nameTab(title)">
              <small>- {{ title }}</small>
            </v-list-item>
          </v-list-group>
        </v-list>
        <template v-slot:append>
            <v-divider class="mx-4" />
            <div class="d-flex flex-column align-center pa-2">
                <h6 class="mt-2">
                    Muebleria - copyright @{{ new Date().getFullYear() }}
                </h6>
                <h6> Version {{ data.version }} </h6>
            </div>
        </template>
      </v-navigation-drawer>
      <v-main class="w-100 position-relative">
        <TabsRoutes :routes="data.nameTabs" :name="data.nameCurrentTab" :index-tab="data.activeTab"/>
        <router-view v-slot="{ Component }">
          <transition name="slide-x-transition">
            <keep-alive>
              <component :is="Component"/>
            </keep-alive>
          </transition>
        </router-view>
      </v-main>
    </v-app>
  </v-layout>
</template>
<script>
import { ref, reactive, computed, onMounted, onUnmounted } from 'vue';
import AppBar from './components/layout/AppBar.vue'
import TabsRoutes from './components/widgets/TabsRoutes.vue';
import LoginAuth from './components/login/LoginAuth.vue';
import environment from './helpers/environment.js';
import { useStore } from './store';

export default {
  mounted() {
    const lastRoute = sessionStorage.getItem('lastRoute')
    this.data.nameTabs = []
    if (lastRoute) {
      this.data.nameTabs.push(lastRoute);
      this.data.activeTab = this.data.nameTabs.indexOf(lastRoute)
      this.data.selectedItems.splice(this.data.selectedItems.length - 1)
      this.data.selectedItems.push(lastRoute)
    }
  },
  setup() {
    const store = useStore()
    const isLoggeInd = computed(() => store.isLoggedIn)
    const screenWidth = ref(window.innerWidth)
    const isMobile = computed(() => screenWidth.value < 600)
    const updateScreen = () => {
      screenWidth.value = window.innerWidth
    }

    onMounted(() => {
      window.addEventListener('resize', updateScreen)
    })

    onUnmounted(() => {
      window.addEventListener('resize', updateScreen)
    })

    const data = reactive({
      drawer: true,
      ventasActions: [
        ['Facturación', 'Facturacion'],
        ['Cuentas por Cobrar', 'CPC'],
      ],
      compraActions: [
        ['Proveedores', 'Proveedores'],
        ['Órdenes de compra', 'Ordenes']
      ],
      managerStock: [
        ['Categorías  Productos', 'Categorias'],
        ['Productos', 'Productos'],
        ['Movimientos', 'Movimientos']
      ],
      clientes: [
        ['Clientes'],
      ],
      accesos: [
        ['Roles'],
        ['Usuarios'],
      ],
      selectedItems: ['Inicio'],
      nameTabs: [],
      nameCurrentTab: '',
      activeTab: null,
      visible: false,
      version: environment.version
    })

    return {
      data,
      isMobile,
      isLoggeInd
    }
  },

  methods: {
    toggleDrawer() {
      this.data.drawer = !this.data.drawer
    },

    nameTab(name) {
      this.data.selectedItems.splice(this.data.selectedItems.length - 1)
      this.data.nameCurrentTab = name
      this.data.visible = true
      const existElement = this.data.nameTabs.indexOf(name)
      sessionStorage.setItem('lastRoute', name);

      if (existElement === -1) {
        this.data.nameTabs.push(name);
        this.data.activeTab = this.data.nameTabs.indexOf(name)
        this.data.selectedItems.push(name)
      } else {
        this.data.activeTab = existElement
        this.data.selectedItems.push(name)
      }

      this.$router.push({ name: name })
    },

    clearApp() {
      this.$router.push({ path: '/' })
      this.data.nameTabs = []
      this.data.nameCurrentTab = ''
      this.data.activeTab = null
      this.data.visible = false
    }
  },

  components: {
    AppBar,
    TabsRoutes,
    LoginAuth
  }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap');

#app{
  font-family: Poppins;
}

.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
