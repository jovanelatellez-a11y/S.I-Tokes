<template>
    <div>
        <v-card elevation="0" class="border" rounded="0">
            <template v-slot:prepend>
                <div class="d-flex align-center">
                <!-- Título -->
                    <div class="font-weight-bold d-flex align-center">
                        <v-icon class="me-2" color="red-darken-4">mdi-shape-outline</v-icon>
                        <small v-if="isMobile">Categorías y SubCategoría</small>
                        <span v-else class="text-h6 font-weight-bold">Categorías y SubCategoría</span>
                    </div>
                </div>
            </template>
            <template v-slot:append>
                <v-btn icon color="red-darken-4" class="mr-2" variant="text" @click="openDialog('sub', 'create', null)">
                    <v-icon>mdi-tag-plus</v-icon>
                    <v-tooltip activator="parent" location="left">Agregar Sub Categoría</v-tooltip> 
                </v-btn>
                <v-btn icon color="red-darken-4" variant="tonal" @click="openDialog('cat', 'create', null)">
                    <v-icon>mdi-tag-multiple</v-icon>
                    <v-tooltip activator="parent" location="left">Agregar Categoría</v-tooltip> 
                </v-btn>
            </template>
            <v-divider />
            <v-card-text class="pa-0 ma-1">
                <v-row dense class="px-0" style="margin: 0;">
                    <v-col cols="6" sm="6" md="6">
                        <v-text-field v-model="data.search" density="compact" variant="outlined" label="Buscar" hide-details placeholder="Buscar textos" persistent-placeholder/>
                    </v-col>
                    <v-col cols="6" md="6" sm="6" class="d-flex justify-end align-center">
                        <v-btn icon color="green" size="small" variant="text" class="mr-2 border" @click="getCategorias()">
                            <v-icon>mdi-refresh</v-icon>
                        </v-btn>
                        <v-btn icon color="grey" size="small" variant="text" class="border" @click="clearData()">
                            <v-icon>mdi-broom</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row dense class="w-100">
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center mb-2">
                    <v-divider /> 
                    <span class="mx-6 text-grey">Registros</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :loading="data.loading" :search="data.search" :mobile="isMobile" class="border" :headers="data.headers" density="compact" :items="data.items">
                    <template v-slot:item.opc="{ item }">
                        <v-tooltip text="Editar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="green" @click="openDialog(`${item.tipo !== 'Categoría' ? 'sub' : 'cat'}`, 'edit', item)" class="mr-1" >mdi-pencil</v-icon>
                            </template>
                        </v-tooltip>
                        
                        <v-tooltip text="Eliminar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="error" class="mr-1" @click="showAlert(item)">mdi-delete</v-icon>
                            </template>
                        </v-tooltip>

                        <v-tooltip text="Ver" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="openDialog(`${item.tipo !== 'Categoría' ? 'sub' : 'cat'}`, 'view', item)">mdi-eye</v-icon>
                            </template>
                        </v-tooltip>
                    </template>
                    <template v-slot:item.fechaRegistro="{ item }">
                        <div>{{ formatedDate(item.fechaRegistro) }}</div>
                    </template>
                    <template v-slot:item.estado="{ item }">
                        <v-chip :color="item.estado ? 'green' : 'error'" :text="item.estado ? 'Activo' : 'Inactivo'"/>
                    </template>
                </v-data-table>                
            </v-card-text>
        </v-card>

        <NewCategoria :show="data.newCat.show" :editar="data.newCat.editar" :title="data.newCat.title" 
            :cat="data.newCat.item" :ver="data.newCat.ver" @closeDialog="closeDialog"/>
        <NewSubCat :show="data.newSubCat.show" :editar="data.newSubCat.editar" :title="data.newSubCat.title" 
            :subCat="data.newSubCat.item" :ver="data.newSubCat.ver" @closeDialog="closeDialogSubCat"/>
        <AlertComp :show="data.viewAlert" @deleteItem="deleteAction"/>
    </div>
</template>

<script>
import { computed, onMounted, onUnmounted, reactive, ref } from 'vue';
import NewCategoria from './modalsCategorias/NewCategoria.vue';
import NewSubCat from './modalsCategorias/NewSubCat.vue';
import RequestHttp from '@/services/requestHttp';
import { formatters } from '@/helpers/formatters';
import AlertComp from '@/components/widgets/AlertComp.vue';

export default {
    mounted() {
        this.getCategorias()
    },

    components: {
        NewCategoria,
        NewSubCat, AlertComp
    },

    setup() {
        const screenWidth = ref(window.innerWidth)
        const isMobile = computed(() => screenWidth.value <= 850)
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
            headers: [
                {title: '', key: 'opc', align: 'center'},
                {title: 'Tipo', key: 'tipo', align: 'center'},
                {title: 'Nombre', key: 'nombre', align: 'center'},
                {title: 'Fecha Registro', key: 'fechaRegistro', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            items: [],
            newCat: {
                show: false,
                editar: false,
                ver: false,
                title: '',
                item: {}
            },
            newSubCat: {
                show: false,
                editar: false,
                ver: false,
                title: '',
                item: {}
            },
            loading: false,
            selectedItem: null,
            search: null,
            viewAlert: false,
            requestHttp: new RequestHttp()
        })

        return {
            isMobile,
            data
        }
    },

    methods: {
        async getCategorias() {
            this.data.items = []
            this.data.loading = true
            const result = await this.data.requestHttp.getCategorias()
            const result2 = await this.data.requestHttp.getSubCategorias()
            this.data.loading = false
            if (result !== null) {
                result.map(item => {
                    this.data.items.push({
                        idCategoriaProducto: item.idCategoriaProducto,
                        tipo: 'Categoría',
                        nombre: item.nombre,
                        fechaRegistro: item.fechaRegistro,
                        estado: item.estado,
                        subCaterias: item.subCategoriaProds,
                        usuarioRegistro: item.usuarioRegistro
                    })
                })
                result2.map(item => {
                    
                    this.data.items.push({
                        idSubCatProd: item.idSubCatProd,
                        idCategoriaProducto: item.idCategoriaProducto,
                        tipo: 'Sub Categoría',
                        nombre: item.nombre,
                        fechaRegistro: item.fechaRegistro,
                        estado: item.estado,
                        subCaterias: item.subCategoriaProds,
                        usuarioRegistro: item.usuarioRegistro,
                        idCategoriaProductoNavigation: item.idCategoriaProductoNavigation,
                        productos: item.productos
                    })
                })
            } else {
                console.log('Error en la solicitud');
            }
        },

        openDialog(comp, type, item = null) {
            if (comp === 'cat') {
                this.data.newCat.show = true
                switch(type) {
                    case 'create': this.data.newCat.ver = false
                                    this.data.newCat.editar = false
                                    this.data.newCat.title = 'Nueva Categoría'
                                    break;
                    case 'edit':  this.data.newCat.ver = false
                                    this.data.newCat.editar = true
                                    this.data.newCat.item = item
                                    this.data.newCat.title = 'Editar Categoría'
                                    break;
                    case 'view':  this.data.newCat.ver = true
                                    this.data.newCat.editar = false
                                    this.data.newCat.item = item
                                    this.data.newCat.title = 'Categoría'
                                    break;
                    default: break;
                }
            } else if (comp === 'sub') {
                this.data.newSubCat.show = true
                switch(type) {
                    case 'create': this.data.newSubCat.ver = false
                                    this.data.newSubCat.editar = false
                                    this.data.newSubCat.title = 'Nuevo Sub Categoría'
                                    break;
                    case 'edit':  this.data.newSubCat.ver = false
                                    this.data.newSubCat.editar = true
                                    this.data.newSubCat.item = item
                                    this.data.newSubCat.title = 'Editar Sub Categoría'
                                    break;
                    case 'view':  this.data.newSubCat.ver = true
                                    this.data.newSubCat.editar = false
                                    this.data.newSubCat.item = item
                                    this.data.newSubCat.title = 'Sub Categoría'
                                    break;
                    default: break;
                }
            }
        },

        formatedDate(dateString) {
            const value = formatters.formatDate(dateString)
            return value
        },

        deleteAction(val) {
            if (val === true) {
                this.deleteItem()
            }
            this.data.viewAlert = false
        },

        showAlert(item){
            this.data.viewAlert = true
            this.data.selectedItem = item
        },

        async deleteItem() {
            if (this.data.selectedItem.tipo === 'Categoría' ) {
                const result = await this.data.requestHttp.deleteCategorias(this.data.selectedItem.idCategoriaProducto)
                if (result !== null) {
                    alert('Categoría Eliminada')
                    this.getCategorias()
                } else {
                    alert('No se pudo eliminar el registro')
                }
            } else if(this.data.selectedItem.tipo === 'Sub Categoría') {
                console.log(this.data.selectedItem.idSubCatProd);
                const result = await this.data.requestHttp.deleteSubCategorias(this.data.selectedItem.idSubCatProd)
                console.log(result);
                
                if (result !== null) {
                    alert('Sub Categoría Eliminado')
                    this.getCategorias()
                } else {
                    alert('No se pudo eliminar el registro')
                }
            }
        },

        closeDialog(val) {
            this.data.newCat.show = val
            this.data.newCat.item = {}
            this.data.newCat.title = ''
            this.data.newCat.editar = val
            this.data.newCat.ver = val
            this.getCategorias()
        },

        closeDialogSubCat (val) {
            this.data.newSubCat.show = val
            this.data.newSubCat.item = {}
            this.data.newSubCat.title = ''
            this.data.newSubCat.editar = false
            this.data.newSubCat.ver = false
            this.getCategorias()
        },

        clearData() {
            this.data.search = null
        }
    }
}
</script>

<style scoped>

</style>