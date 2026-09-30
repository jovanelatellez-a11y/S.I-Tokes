<template>
  <div>
    <v-dialog v-model="localShow" max-width="750" persistent>
      <v-card class="rounded-lg">
        <v-card-title class="font-weight-bold text-center bg-red-darken-4 text-white">
          <v-icon class="me-2">mdi-eye</v-icon> Detalles del Producto
        </v-card-title>

        <v-tabs v-model="tab" color="red-darken-4" density="compact" class="mt-2">
          <v-tab height="25" density="compact" class="border custom-border"><small>Detalles del Producto</small></v-tab>
          <v-tab height="25" density="compact" class="border custom-border"><small>Detalles del Proveedor</small></v-tab>
        </v-tabs>
        <v-divider class="custom-margin"/>
        <v-card-text>
          <v-window v-model="tab">
            <!-- Detalles del Producto -->
            <v-window-item>
              <v-row class="mt-2 d-flex justify-space-between align-center" dense>
                <v-col cols="12" sm="4" class="text-center mb-2">
                  <v-img class="rounded-lg border" :src="producto.imgBase64" height="160" cover></v-img>
                </v-col>

                <v-col cols="12" sm="8" class="d-flex justify-center align-center">
                  <v-row dense>
                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-barcode</v-icon>
                        <span class="text-caption"><strong>Código:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.codigo }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-tag</v-icon>
                        <span class="text-caption"><strong>Nombre:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.nombre }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-currency-usd</v-icon>
                        <span class="text-caption"><strong>Precio:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ formateCurrency(producto.precio) }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-currency-usd</v-icon>
                        <span class="text-caption"><strong>Costo:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ formateCurrency(producto.costo) }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-folder-outline</v-icon>
                        <span class="text-caption"><strong>Sub Categoría:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.categoria }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-shape-outline</v-icon>
                        <span class="text-caption"><strong>Tipo:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.tipoProducto }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-warehouse</v-icon>
                        <span class="text-caption"><strong>Stock:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.cantidadTotal }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-warehouse</v-icon>
                        <span class="text-caption"><strong>Stock:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.cantidadMinima }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-check-circle-outline</v-icon>
                        <span class="text-caption"><strong>Estado:</strong></span>
                      </div>
                      <!-- <v-chip density="compact" :color="producto.estado ? 'green' : 'error'" :text="producto.estado ? 'Activo' : 'Inactivo'"></v-chip> -->
                      <div class="ms-6 text-caption">{{ producto.estado ? 'Activo' : 'Inactivo' }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-account</v-icon>
                        <span class="text-caption"><strong>Registrado por:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ producto.usuarioRegistro }}</div>
                    </v-col>

                    <v-col cols="6" sm="4" class="d-flex flex-column align-start py-1 ">
                      <div class="d-flex align-center">
                        <v-icon size="small" class="me-1" color="red-darken-4">mdi-calendar</v-icon>
                        <span class="text-caption"><strong>Fecha Registro:</strong></span>
                      </div>
                      <div class="ms-6 text-caption">{{ formateDate(producto.fechaRegistro)  }}</div>
                    </v-col>
                  </v-row>
                </v-col>
              </v-row>
            </v-window-item>

            <!-- Detalles del Proveedor -->
            <v-window-item>
              <v-row class="mt-4" dense>
                <!-- Imagen del Producto -->
                <v-col cols="12" md="6" sm="6" class="mb-2">
                  <div class="d-flex align-center">
                    <v-icon class="me-2" color="red-darken-4">mdi-truck</v-icon>
                    <strong>Proveedor Principal:</strong>
                  </div>
                  <v-list density="compact" v-if="data.proveedoresProducto && data.proveedoresProducto.length" >
                    <v-list-item density="compact" color="indigo" v-for="(prov, index) in data.proveedoresProducto.filter(p => p.predeterminado)" :key="index">
                      <template v-slot:prepend>
                        <v-icon color="indigo" size="small">mdi-check-decagram</v-icon>
                      </template>
                      <v-list-item-title>{{ prov.nombre }}</v-list-item-title>
                    </v-list-item>
                  </v-list>
                  <span class="ms-6" v-else>N/A</span>
                </v-col>
                <v-col cols="12" md="6" sm="6" class="mb-2">
                  <div class="d-flex justify-space-between align-center">
                    <div class="d-flex align-center">
                      <v-icon class="me-2" color="red-darken-4">mdi-truck-delivery</v-icon>
                      <strong>Proveedores:</strong>
                    </div>
                  </div>
                  <ul class="ms-6 mt-2" v-if="data.proveedoresProducto && data.proveedoresProducto.length">
                    <li v-for="(prov, index) in data.proveedoresProducto" :key="index">{{ prov.nombre }}</li>
                  </ul>
                  <span class="ms-6" v-else>N/A</span>
                </v-col>
                <v-col cols="12" sm="12" md="12" class="d-flex justify-end align-end">
                  <v-btn color="indigo-darken-4" icon="mdi-plus" variant="elevated" size="small" @click="abrirModalProveedor" />
                </v-col>
              </v-row>
            </v-window-item>
          </v-window>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions class="justify-end">
          <v-btn color="grey" variant="outlined" @click="closeDialog">Cerrar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- MODAL PARA AGREGAR PROVEEDOR SECUNDARIO -->
    <v-dialog v-model="modalProveedor" max-width="600">
      <v-card class="rounded-lg">
        <v-card-title class="font-weight-bold text-center bg-red-darken-4 text-white">
          <v-icon class="mr-2">mdi-account-plus</v-icon>
          Agregar Proveedor Secundario
        </v-card-title>
        <v-divider />
        <v-card-text>
          <v-row dense>
            <v-col cols="12" md="6">
              <v-autocomplete v-model="data.nuevoProveedor.idProveedor" label="Proveedor" prepend-inner-icon="mdi-account" 
                variant="outlined" density="compact" hide-details :items="data.proveedores"/>
            </v-col>
            <v-col cols="12" md="6">
              <v-textarea v-model="data.observaciones" label="Observaciones" prepend-inner-icon="mdi-text" 
                variant="outlined" density="compact" :rows="1" hide-details/>
            </v-col>
            <v-col cols="12" md="12">
              <v-checkbox color="indigo" density="compact" v-model="data.nuevoProveedor.predeterminado" label="Predeterminado" hide-details/>
            </v-col>
          </v-row>
        </v-card-text>
        <v-divider />
        <v-card-actions class="justify-end">
          <v-btn color="grey" variant="outlined" @click="closeModalProveedor()">Cancelar</v-btn>
          <v-btn class="bg-red-darken-4 text-white" variant="elevated" @click="postProveedorProducto()">Agregar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { reactive, ref, watch } from 'vue'
import { formatters } from '@/helpers/formatters'
import RequestHttp from '@/services/requestHttp'

export default {
  name: 'ProductDetailsDialog',

  mounted() {
    this.getProveedores()
    // this.getProveedoresProducto()
  },

  props: {
    show: Boolean,
    producto: Object,
  },

  setup(props) {
    const tab = ref(0)

    const getProvProduct = async () =>  {
      data.proveedoresProducto = []
      const dataPro = []
      const result = await data.requestHttp.getProveedorProductos()
      result.map(item => {
        console.log(item);
        
        dataPro.push({
          nombre: item.proveedor, 
          idProveedor: item.idProveedor,
          idProducto: item.idProducto,
          predeterminado: item.predeterminado,
          idProveedorProducto: item.idProveedorProducto
        })
      })

      data.proveedoresProducto = dataPro.filter(item => item.idProducto === props.producto.idProducto)
      console.log(dataPro, data.proveedoresProducto);
    }

    const localShow = ref(props.show)
    watch(() => props.show, (val) => {
      localShow.value = val

      if (val) {        
        getProvProduct()
      }
      if (val) tab.value = 0
    })

    const modalProveedor = ref(false)
    const data = reactive({
      proveedores: [],
      proveedoresProducto: [],
      nuevoProveedor: {
        idProveedor: null,
        idProducto: null,
        predeterminado: false,
        usuarioRegistro: 1,
      },
      observaciones: null,
      requestHttp: new RequestHttp()
    })

    return {
      tab,
      localShow,
      modalProveedor,
      data
    }
  },

  methods: {
    async getProveedores() {
      this.data.proveedores = []
      const result = await this.data.requestHttp.getProveedores()
      result.map(item => {
        this.data.proveedores.push({title: item.nombre, value: item.idProveedor})
      })
    },

    async getProveedoresProducto() {
      this.data.proveedoresProducto = []
      const data = []
      const result = await this.data.requestHttp.getProveedorProductos()
      result.map(item => {
        console.log(item);
        
        data.push({
          nombre: item.nombre, 
          idProveedor: item.idProveedor,
          idProducto: item.idProducto,
          idProveedorProducto: item.idProveedorProducto
        })
      })

      this.data.proveedoresProducto = data.filter(item => item.idProducto === this.producto.idProducto)
      console.log(data, this.data.proveedoresProducto);
      
    },

    async postProveedorProducto() {
      if (!this.data.nuevoProveedor.idProveedor) {
        alert('Elija un proveedor')
        return
      }
      this.data.nuevoProveedor.idProducto = this.producto.idProducto
      const result = await this.data.requestHttp.postProveedorProducto(this.data.nuevoProveedor)
      if (result !==  null) {
        alert('Proveedor agreado')
      } else {
        alert('No se pudo agregar el proveedor')
        return
      }

      this.modalProveedor = false
    },



    closeDialog() {
      this.$emit('cerrarDialog', false)
    },

    abrirModalProveedor() {
      this.modalProveedor = true
    },

    closeModalProveedor() {
      this.data.nuevoProveedor.idProveedor = null
      this.data.observaciones = null
      this.data.nuevoProveedor.predeterminado = false
      this.modalProveedor = false
    },

    formateDate(dateString) {
      const value = formatters.formatDate(dateString)
      return value
    },

    formateCurrency(key) {
      const value = formatters.formatCurrency(key)
      return value
    }
  },
}
</script>

<style scoped>
.custom-border{
  border-top-left-radius: 5px !important;
  border-top-right-radius: 5px !important;
}

.custom-margin{
  margin-top: -10px;
}
</style>
