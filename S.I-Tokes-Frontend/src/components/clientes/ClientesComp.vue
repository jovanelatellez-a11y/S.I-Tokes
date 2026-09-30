<template>
    <div>
        <v-card elevation="0" class="border" rounded="0">
            <template v-slot:prepend>
                <div class="d-flex align-center">
                <!-- Título -->
                    <div class="font-weight-bold d-flex align-center">
                        <v-icon class="me-2" color="red-darken-4">mdi-account-multiple</v-icon>
                        <small v-if="isMobile">Clientes</small>
                        <span v-else class="text-h6 font-weight-bold">Clientes</span>
                    </div>
                </div>
            </template>
            <template v-slot:append>
                <v-btn icon color="red-darken-4" class="mr-2" variant="text" @click="openDialog('tipo', 'create', null)">
                    <v-icon>mdi-account-plus</v-icon>
                    <v-tooltip activator="parent" location="left">Agregar Tipo Clientes</v-tooltip> 
                </v-btn>
                <v-btn icon color="red-darken-4" variant="tonal" @click="openDialog('prov', 'create', null)">
                    <v-icon>mdi-account-outline</v-icon>
                    <v-tooltip activator="parent" location="left">Agregar Cliente</v-tooltip> 
                </v-btn>
            </template>
            <v-divider />
            <v-card-text class="py-2 px-0">
                <v-row dense class="px-0" style="margin: 0;">
                    <v-col cols="6" sm="6" md="6">
                        <v-text-field v-model="data.search" density="compact" variant="outlined" label="Buscar" hide-details placeholder="Buscar textos" persistent-placeholder/>
                    </v-col>
                    <v-col cols="6" md="6" sm="6" class="d-flex justify-end align-center">
                        <v-btn icon color="green" size="small" variant="text" class="mr-2 border" @click="refresData()">
                            <v-icon>mdi-refresh</v-icon>
                        </v-btn>
                        <v-btn icon color="grey" size="small" variant="text" class="border" @click="data.search = null">
                            <v-icon>mdi-broom</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row dense class="w-100">
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center mb-2">
                    <v-divider /> 
                    <span class="mx-6 text-grey font-weight-bold">Clientes</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :loading="data.loading" :search="data.search" :mobile="isMobile" class="border" :headers="data.headers" density="compact" :items="data.items">
                    <template v-slot:item.fechaRegistro="{ item }">
                        <div>{{ formateDate(item.fechaRegistro) }}</div>
                    </template>
                    <template v-slot:item.idCategoriaClienteNavigation="{ item }">
                        <div>{{ item.idCategoriaClienteNavigation.nombre }}</div>
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
                    <span class="mx-6 text-grey font-weight-bold">Tipo de Clientes</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :loading="data.loadingTipo" :search="data.search" :mobile="isMobile" class="border" :headers="data.headersCat" density="compact" :items="data.itemsCat">
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

        <NewCliente :show="data.newCliente.show" :editar="data.newCliente.editar" :title="data.newCliente.title" 
            :prov="data.newCliente.item" :ver="data.newCliente.ver" @closeDialog="closeDialog"/>
        <NewTipoCliente :show="data.newCatCliente.show" :editar="data.newCatCliente.editar" :title="data.newCatCliente.title" 
            :prov="data.newCatCliente.item" :ver="data.newCatCliente.ver" @closeDialog="closeDialogCat"/>
        <AlertComp :show="data.viewAlert" @deleteItem="deleteAction"/>
    </div>
</template>

<script>
import { computed, onMounted, onUnmounted, reactive, ref } from 'vue';
import { formatters } from '@/helpers/formatters';
import NewCliente from './dialogsClientes/NewCliente.vue';
import NewTipoCliente from './dialogsClientes/NewTipoCliente.vue';
import RequestHttp from '@/services/requestHttp';
import AlertComp from '@/components/widgets/AlertComp.vue';

export default {
    mounted() {
        this.getClientes()
        this.getCategoriaClientes()
    },

    components: {
        NewCliente,
        AlertComp,
        NewTipoCliente
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
                {title: 'Cliente', key: 'codigo', align: 'center'},
                {title: 'T. Cliente', key: 'idCategoriaClienteNavigation', align: 'center'},
                {title: 'Departamento', key: 'departamento', align: 'center'},
                {title: 'Teléfono', key: 'telefono', align: 'center'},
                {title: 'Fecha Registro', key: 'fechaRegistro', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            headersCat: [
                {title: '', key: 'opc', align: 'center'},
                {title: 'Nombre', key: 'nombre', align: 'center'},
                {title: 'Descripcion', key: 'descripcion', align: 'center'},
                {title: 'Fecha Registro', key: 'fechaRegistro', align: 'center'},
                {title: 'Usuario Registro', key: 'usuarioRegistro', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            items: [],
            itemsCat: [],
            newCliente: {
                show: false,
                editar: false,
                ver: false,
                title: '',
                item: {}
            },
            newCatCliente: {
                show: false,
                editar: false,
                ver: false,
                title: '',
                item: {}
            },
            selectedItem: null,
            search: null,
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
        async getClientes() {
            this.data.items = []
            this.data.loading = true
            const result = await this.data.requestHttp.getClientes()
            this.data.loading = false
            result.map(item => {
                this.data.items.push(item)
            })
        },
        async getCategoriaClientes() {
            this.data.itemsCat = []
            this.data.loadingTipo = true
            const result = await this.data.requestHttp.getCategoriaClientes()
            this.data.loadingTipo = false
            result.map(item => {
                this.data.itemsCat.push(item)
            })
        },

        openDialog(comp, type, item = null) {
            if (comp === 'prov') {
                this.data.newCliente.show = true
                switch(type) {
                    case 'create': this.data.newCliente.ver = false
                                    this.data.newCliente.editar = false
                                    this.data.newCliente.title = 'Nuevo Cliente'
                                    break;
                    case 'edit':  this.data.newCliente.ver = false
                                    this.data.newCliente.editar = true
                                    this.data.newCliente.item = item
                                    this.data.newCliente.title = 'Editar Cliente'
                                    break;
                    case 'view':  this.data.newCliente.ver = true
                                    this.data.newCliente.editar = false
                                    this.data.newCliente.item = item
                                    this.data.newCliente.title = 'Cliente'
                                    break;
                    default: break;
                }
            } else if (comp === 'tipo') {
                this.data.newCatCliente.show = true
                switch(type) {
                    case 'create': this.data.newCatCliente.ver = false
                                    this.data.newCatCliente.editar = false
                                    this.data.newCatCliente.title = 'Nuevo Tipo Cliente'
                                    break;
                    case 'edit':  this.data.newCatCliente.ver = false
                                    this.data.newCatCliente.editar = true
                                    this.data.newCatCliente.item = item
                                    this.data.newCatCliente.title = 'Editar Tipo Cliente'
                                    break;
                    case 'view':  this.data.newCatCliente.ver = true
                                    this.data.newCatCliente.editar = false
                                    this.data.newCatCliente.item = item
                                    this.data.newCatCliente.title = 'Tipo de Cliente'
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
            
            if (this.data.selectedItem.idCliente) {
                const result = await this.data.requestHttp.deleteCliente(this.data.selectedItem.idCliente)
                if (result !== null) {
                    alert('Cliente Eliminado')
                    this.getClientes()
                } else {
                    alert('No se pudo eliminar el registro')
                }
            } else {
                const result = await this.data.requestHttp.deleteCategoriaCliente(this.data.selectedItem.idCategoriaCliente)
                if (result !== null) {
                    alert('Registro Eliminado')
                    this.getCategoriaClientes()
                } else {
                    alert('No se pudo eliminar el registro')
                }
            }
        },

        closeDialog(val) {
            this.data.newCliente.show = val
            this.data.newCliente.item = {}
            this.data.newCliente.title = ''
            this.data.newCliente.editar = false
            this.data.newCliente.ver = false
            this.getClientes()
        },

        refresData() {
            this.getClientes()
            this.getCategoriaClientes()
        },

        closeDialogCat (val) {
            this.data.newCatCliente.show = val
            this.data.newCatCliente.item = {}
            this.data.newCatCliente.title = ''
            this.data.newCatCliente.editar = false
            this.data.newCatCliente.ver = false
            this.getCategoriaClientes()
        }
    }
}
</script>

<style scoped>

</style>