<template>
    <div class="w-100">
        <!-- Tabs dinamicas para la creacion de rutas -->
        <!-- De acuerdo al orden de los items, se muestra al correspondiente ruta -->
        <v-tabs v-model="activeTab" color="red-darken-4" density="compact" class="mt-2">
            <v-tab draggable="true" @dragstart="onDragstart(index)" @dragover.prevent @drop="onDrop(index)" height="25" @click="redirectOpc(name)" 
                v-for="(name, index) in routes" :key="index" density="compact" class="border custom-border">
                <small>{{ name }}</small> <v-icon @click.stop="closeTab(index)" size="14" class="ml-2">mdi-close-circle</v-icon>
            </v-tab>
        </v-tabs>
    </div>
</template>

<script>
export default {
    props: {
        routes: {
            type: Array,
            required: true
        },
        name: {
            type: String,
            required: true
        },
        indexTab: {
            type: Number,
            required: false
        }
    },

    data() {
        return {
            activeTab: this.indexTab,
            draggedIndex: null
        }
    },

    watch: {
        indexTab(newIndex) {
            this.activeTab = newIndex
        },

        "$route.name"(newRoute) {
            const index = this.routes.indexOf(newRoute)
            this.activeTab = index !== -1 ? index : 0;
        }
    },
    
    methods: {
        onDragstart(i) {
            this.draggedIndex = i
        },

        redirectOpc(name) {
            let arr = this.routes
            
            this.activeTab = arr.indexOf(name)
            this.$router.push({ name })
        },

        closeTab(index) {
            
            let arr = this.routes;
            sessionStorage.removeItem('lastRoute')

            if (index === arr.length - 1) {
                // Caso #1: Si el tab a cerrar es el último
                if (this.activeTab == index) {
                    this.activeTab = index - 1
                    this.$router.push({ name: arr[index - 1] })
                    sessionStorage.setItem('lastRoute', arr[index - 1])
                }
            } else if(this.activeTab === index) {
                // Caso #2: Si el tab a cerrar es el activo
                this.activeTab = index
                this.$router.push({ name: arr[index + 1] })
                sessionStorage.setItem('lastRoute', arr[index + 1])
            } else {
                // Caso #3: ajustar el indice si la activa esta despues de la cerrada
                this.activeTab -=  1;
            }

            arr.splice(index, 1)
            if (arr.length === 0) {
                this.$router.push({path: '/'})
                sessionStorage.removeItem('lastRoute')
                this.$emit('isVisible', true)
            }
        },

        onDrop(indexDrop) {
            if(this.draggedIndex === null || this.draggedIndex === indexDrop ) return;

            const movedItem = this.routes[this.draggedIndex];
            this.routes.splice(this.draggedIndex, 1) // eliminamos el tab movido
            this.routes.splice(indexDrop, 0, movedItem) // insertamos el tab en su nueva position

            if (this.activeTab === this.draggedIndex) {
                this.activeTab = indexDrop
            } else if (this.activeTab > this.draggedIndex && this.activeTab <= indexDrop) {
                this.activeTab -= 1
            } else if (this.activeTab < this.draggedIndex && this.activeTab >= indexDrop) {
                this.activeTab += 1
            }

            this.draggedIndex == null
        }
    }
}
</script>

<style scoped>
.v-tabs{
    height: 24px;
}

.custom-border{
    border-top-left-radius: 5px !important;
    border-top-right-radius: 5px !important;
}
</style>