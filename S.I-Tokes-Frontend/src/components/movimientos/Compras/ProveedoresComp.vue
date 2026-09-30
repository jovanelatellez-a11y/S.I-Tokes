<template>
    <div>
        <v-card elevation="0" class="border" rounded="0">
            <template v-slot:prepend>
                <div class="d-flex align-center">
                <!-- Título -->
                    <div class="font-weight-bold d-flex align-center">
                        <v-icon class="me-2" color="red-darken-4">mdi-package-variant</v-icon>
                        <small v-if="isMobile">Gestión de Proveedores</small>
                        <span v-else class="text-h6 font-weight-bold">Gestión de Proveedores</span>
                    </div>
                </div>
            </template>
            <template v-slot:append>
                <v-btn icon color="red-darken-4" class="mr-2" variant="text" @click="openDialog('tipo', 'create', null)">
                    <v-icon>mdi-account-plus</v-icon>
                    <v-tooltip activator="parent" location="left">Agregar Tipo Proveedores</v-tooltip> 
                </v-btn>
                <v-btn class="bg-red-darken-4 rounded" @click="openDialog('prov', 'create', null)">
                    <v-icon>mdi-plus</v-icon>
                    <v-tooltip activator="parent" location="left">Agregar Proveedores</v-tooltip> 
                </v-btn>
            </template>
            <v-divider />
            <v-card-text class="py-2 px-0">
                <v-row dense class="px-0" style="margin: 0;">
                    <v-col cols="6" sm="6" md="6">
                        <v-text-field v-model="data.search" density="compact" variant="outlined" label="Buscar" hide-details placeholder="Buscar textos" persistent-placeholder/>
                    </v-col>
                    <v-col cols="6" md="6" sm="6" class="d-flex justify-end align-center">
                        <v-btn icon color="green" size="small" variant="text" class="mr-2 border" @click="refreshData()">
                            <v-icon>mdi-refresh</v-icon>
                        </v-btn>
                        <v-btn icon color="grey" size="small" variant="text" class="border">
                            <v-icon>mdi-broom</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row dense class="w-100">
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center mb-2">
                    <v-divider /> 
                    <span class="mx-6 text-grey font-weight-bold">Proveedores</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :loading="data.loading" :search="data.search" :mobile="isMobile" class="border" :headers="data.headers" density="compact" :items="data.items">
                    <template v-slot:item.fechaRegistro="{ item }">
                        <div>{{ formateDate(item.fechaRegistro) }}</div>
                    </template>
                    <template v-slot:item.tipoProveedor="{ item }">
                        <div>{{ item.idTipoProveedorNavigation.nombre }}</div>
                    </template>
                    <template v-slot:item.opc="{ item }">
                        <v-tooltip text="Editar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="green" @click="openDialog('prov', 'edit', item)" class="mr-1" >mdi-pencil</v-icon>
                            </template>
                        </v-tooltip>
                        
                        <v-tooltip text="Eliminar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="error" @click="showAlert(item)" class="mr-1">mdi-delete</v-icon>
                            </template>
                        </v-tooltip>

                        <v-tooltip text="Ver" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="openDialog('prov', 'view', item)">mdi-eye</v-icon>
                            </template>
                        </v-tooltip>
                    </template>
                    <template v-slot:item.estado="{ item }">
                        <v-chip :color="item.estado ? 'green' : 'error'" :text="item.estado ? 'Activo' : 'Inactivo'"/>
                    </template>
                </v-data-table>
                <v-card-subtitle class="d-flex align-center text-center my-2">
                    <v-divider /> 
                    <span class="mx-6 text-grey font-weight-bold">Tipo de Proveedores</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :loading="data.loadingTipo" :search="data.search" :mobile="isMobile" class="border" :headers="data.headersTipoProv" density="compact" :items="data.itemsTipoProv">
                    <template v-slot:item.fechaRegistro="{ item }">
                        <div>{{ formateDate(item.fechaRegistro) }}</div>
                    </template>
                    <template v-slot:item.opc="{ item }">
                        <v-tooltip text="Editar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="green" @click="openDialog('tipo', 'edit', item)" class="mr-1" >mdi-pencil</v-icon>
                            </template>
                        </v-tooltip>
                        
                        <v-tooltip text="Eliminar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" @click="showAlert(item)" color="error" class="mr-1">mdi-delete</v-icon>
                            </template>
                        </v-tooltip>

                        <v-tooltip text="Ver" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="openDialog('tipo', 'view', item)">mdi-eye</v-icon>
                            </template>
                        </v-tooltip>
                    </template>
                    <template v-slot:item.estado="{ item }">
                        <v-chip :color="item.estado ? 'green' : 'error'" :text="item.estado ? 'Activo' : 'Inactivo'"/>
                    </template>
                </v-data-table>
            </v-card-text>
        </v-card>

        <NewProveedor :show="data.newProv.show" :editar="data.newProv.editar" :title="data.newProv.title" 
            :prov="data.newProv.item" :ver="data.newProv.ver" @closeDialog="closeDialog"/>
        <NewTipoProv :show="data.newTipoProv.show" :editar="data.newTipoProv.editar" :title="data.newTipoProv.title" 
            :prov="data.newTipoProv.item" :ver="data.newTipoProv.ver" @closeDialog="closeDialogTipoProv"/>
        <AlertComp :show="data.viewAlert" @deleteItem="deleteAction"/>
    </div>
</template>

<script>
import { computed, onMounted, onUnmounted, reactive, ref } from 'vue';
import { formatters } from '@/helpers/formatters';
import NewProveedor from './dialogsCompras/NewProveedor.vue';
import NewTipoProv from './dialogsCompras/NewTipoProv.vue';
import RequestHttp from '@/services/requestHttp';
import AlertComp from '@/components/widgets/AlertComp.vue';

export default {
    mounted() {
        this.getProveedores()
        this.getTipoProveedores()
    },

    components: {
        NewProveedor,
        NewTipoProv,
        AlertComp
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
                {title: 'Proveedor', key: 'nombre', align: 'center'},
                {title: 'T. Proveedor', key: 'tipoProveedor', align: 'center'},
                {title: 'Departamento', key: 'departamento', align: 'center'},
                {title: 'Teléfono', key: 'telefono', align: 'center'},
                {title: 'Fecha Registro', key: 'fechaRegistro', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            items: [],
            headersTipoProv: [
                {title: '', key: 'opc', align: 'center'},
                {title: 'Tipo Proveedor', key: 'nombre', align: 'center'},
                {title: 'Fecha Registro', key: 'fechaRegistro', align: 'center'},
                {title: 'Observaciones', key: 'observaciones', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            itemsTipoProv: [],
            newProv: {
                show: false,
                editar: false,
                ver: false,
                title: '',
                item: {}
            },
            newTipoProv: {
                show: false,
                editar: false,
                ver: false,
                title: '',
                item: {}
            },
            selectedItem: null,
            search: null,
            menuDesde: false,
            menuHasta: false,
            loading: false, 
            loadingTipo: false,
            viewAlert: false,
            requestHttp: new RequestHttp()
        })

        return {
            isMobile,
            data
        }
    },

    methods: {
        async getProveedores() {
            this.data.items = []
            this.data.loading = true
            const result = await this.data.requestHttp.getProveedores()
            this.data.loading = false
            result.map(item => {
                this.data.items.push(item)
            })
        },

        async getTipoProveedores() {
            this.data.itemsTipoProv = []
            this.data.loadingTipo = true
            const result = await this.data.requestHttp.getTipoProveedores()
            this.data.loadingTipo = false
            result.map(item => {
                this.data.itemsTipoProv.push(item)
            })
        },

        openDialog(comp, type, item = null) {
            if (comp === 'prov') {
                this.data.newProv.show = true
                switch(type) {
                    case 'create': this.data.newProv.ver = false
                                    this.data.newProv.editar = false
                                    this.data.newProv.title = 'Nuevo Proveedor'
                                    break;
                    case 'edit':  this.data.newProv.ver = false
                                    this.data.newProv.editar = true
                                    this.data.newProv.item = item
                                    this.data.newProv.title = 'Editar Proveedor'
                                    break;
                    case 'view':  this.data.newProv.ver = true
                                    this.data.newProv.editar = false
                                    this.data.newProv.item = item
                                    this.data.newProv.title = 'Proveedor'
                                    break;
                    default: break;
                }
            } else if (comp === 'tipo') {
                this.data.newTipoProv.show = true
                switch(type) {
                    case 'create': this.data.newTipoProv.ver = false
                                    this.data.newTipoProv.editar = false
                                    this.data.newTipoProv.title = 'Nuevo Tipo Proveedor'
                                    break;
                    case 'edit':  this.data.newTipoProv.ver = false
                                    this.data.newTipoProv.editar = true
                                    this.data.newTipoProv.item = item
                                    this.data.newTipoProv.title = 'Editar Tipo Proveedor'
                                    break;
                    case 'view':  this.data.newTipoProv.ver = true
                                    this.data.newTipoProv.editar = false
                                    this.data.newTipoProv.item = item
                                    this.data.newTipoProv.title = 'Tipo de Proveedor'
                                    break;
                    default: break;
                }
            }
        },

        formateDate(dateString) {
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
            console.log(this.data.selectedItem);
            
            if (this.data.selectedItem.idProveedor) {
                const result = await this.data.requestHttp.deleteProveedor(this.data.selectedItem.idProveedor)
                if (result !== null) {
                    alert('Proveedor Eliminado')
                    this.getProveedores()
                } else {
                    alert('No se pudo eliminar el registro')
                }
            } else {
                
                const result = await this.data.requestHttp.deleteTipoProveedor(this.data.selectedItem.idTipoProveedor)
                if (result !== null) {
                    alert('Registro Eliminado')
                    this.getTipoProveedores()
                } else {
                    alert('No se pudo eliminar el registro')
                }
            }
        },

        refreshData() {
            this.getProveedores()
            this.getTipoProveedores()
        },

        closeDialog(val) {
            this.data.newProv.show = val
            this.data.newProv.item = {}
            this.data.newProv.title = ''
            this.data.newProv.editar = false
            this.data.newProv.ver = false
            this.getProveedores()
        },

        closeDialogTipoProv (val) {
            this.data.newTipoProv.show = val
            this.data.newTipoProv.item = {}
            this.data.newTipoProv.title = ''
            this.data.newTipoProv.editar = false
            this.data.newTipoProv.ver = false
            this.getTipoProveedores()
        }
    }
}
</script>

<style scoped>

</style>