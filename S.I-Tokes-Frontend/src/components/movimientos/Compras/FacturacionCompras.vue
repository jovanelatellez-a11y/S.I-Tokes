<template>
    <div class="w-100">
        <v-card class="border" elevation="0" rounded="0">
            <!-- Encabezado -->
            <template v-slot:prepend>
                <div class="d-flex align-center">
                    <!-- Título -->
                    <div class="text-h6 font-weight-bold d-flex align-center">
                    <v-icon class="me-2" color="red-darken-4">mdi-package-variant</v-icon>
                        Órdenes de Compras
                    </div>
                </div>
            </template>
            <template v-slot:append>
                <v-btn class="bg-red-darken-4 rounded-" @click="createOrden()">
                    <v-icon>mdi-plus</v-icon>
                    <v-tooltip activator="parent" location="left">Nueva Órden</v-tooltip> 
                </v-btn>
            </template>
            <v-divider /> 
            <v-row class="pa-2" dense>
                <v-col cols="6" md="6" sm="6">
                    <v-text-field v-model="data.search" color="red-darken-4" density="compact" variant="outlined" append-inner-icon="mdi-magnify" label="Buscar productos"
                        hide-details placeholder="Ingrese un texto a buscar..." persistent-placeholder/>
                </v-col>
                <v-col cols="6" md="6" sm="6" class="d-flex justify-end align-center">
                    <v-btn icon color="green" size="small" variant="text" class="mr-2 border" @click="getOrdenes()">
                        <v-icon>mdi-refresh</v-icon>
                    </v-btn>
                    <v-btn icon color="grey" size="small" variant="text" class="border" @click="data.search = null">
                        <v-icon>mdi-broom</v-icon>
                    </v-btn>
                </v-col>
            </v-row>

            <v-card-text class="pt-0 px-0">
                <v-card-subtitle class="d-flex align-center text-center mb-2">
                    <v-divider /> 
                    <span class="mx-6 text-grey font-weight-bold">Registros</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :search="data.search" :headers="data.header" :items="data.ordenes" class="border font" density="compact">
                    <template v-slot:item.total="{ item }">
                        <div>{{ formatedCurrency(item.total) }}</div>
                    </template>
                    <template v-slot:item.fechaRegistro="{ item }">
                        <div>{{ formatedDate(item.fechaRegistro) }}</div>
                    </template>
                    <template v-slot:item.aprobada="{ item }">
                        <div>{{ item.aprobada ? 'SI' : 'NO' }}</div>
                    </template>
                    <template v-slot:item.opc="{ item }">
                        <v-tooltip text="Editar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="green" @click="editOrden(item)" class="mr-1" >mdi-pencil</v-icon>
                            </template>
                        </v-tooltip>
                        
                        <v-tooltip text="Eliminar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" @click="showAlert(item)" color="error" class="mr-1">mdi-delete</v-icon>
                            </template>
                        </v-tooltip>

                        <v-tooltip text="Ver" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="viewOrden(item)">mdi-eye</v-icon>
                            </template>
                        </v-tooltip>
                    </template>
                    <template v-slot:item.estado="{ item }">
                        <v-chip :color="item.estado ? 'green' : 'error'" small>
                            {{ item.estado ? 'Activo' : 'Inactivo' }}
                        </v-chip>
                    </template>
                </v-data-table>
            </v-card-text>
        </v-card>
        <NuevaFacturaCompras :show="data.compra.show" :editar="data.compra.editar" :title="data.compra.title" 
            :orden="data.compra.item" @closeDialog="closeDialog" @refreshTable="getOrdenes()"/>
        <ViewOrdenes :show="data.viewOrden.show" :orden="data.viewOrden.item" @closeDialog="closeDialog"/>
        <AlertComp :show="data.viewAlert" @deleteItem="deleteAction"/>
    </div>
</template>

<script>
import { formatters } from '@/helpers/formatters.js';
import { reactive, computed, ref } from 'vue';
import AlertComp from '@/components/widgets/AlertComp.vue';
import NuevaFacturaCompras from './dialogsCompras/NuevaFacturaCompras.vue';
import ViewOrdenes from './dialogsCompras/ViewOrdenes.vue';
import RequestHttp from '@/services/requestHttp';

export default {
    mounted() {
        this.getOrdenes()    
    },

    components: {
        NuevaFacturaCompras,
        ViewOrdenes,
        AlertComp
    },

    setup() {
        const data = reactive({
            header: [
                {title: '', key: 'opc', align: 'center',},
                {title: 'Nº Órden', key: 'noOrden', align: 'center'},
                {title: 'Proveedor', key: 'proveedor', align: 'center'},
                {title: 'Vendedor', key: 'usuarioRegistro', align: 'center'},
                {title: 'Aprobada', key: 'aprobada', align: 'center'},
                {title: 'Monto', key: 'total', align: 'center'},
                {title: 'FechaRegistro', key: 'fechaRegistro', align: 'center'},
                {title: 'Observaciones', key: 'observaciones', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            ordenes: [],
            compra: {
                show: false,
                editar: false,
                item: {},
                title: '',
            },
            loading: false,
            selectedItem: null, 
            viewAlert: false,
            search: null,
            viewOrden: {
                show: false,
                item: {}
            },
            requestHttp: new RequestHttp()
        })

        return {
            data,
        }
    },

    methods: {
        async getOrdenes() {
            this.data.ordenes = []
            this.data.loading = true
            const result = await this.data.requestHttp.getCompras()
            this.data.loading = false
            if (result !== null) {
                result.map(item => {
                    this.data.ordenes.push(item)
                })
            }
        },

        editOrden(item) {
            this.data.compra.show = true
            this.data.compra.editar = true
            this.data.compra.title = 'EDITAR ORDEN DE COMPRA'
            this.data.compra.item = item
        },

        viewOrden(item) {
            this.data.viewOrden.show = true
            this.data.viewOrden.item = item
        },

        createOrden() {
            this.data.compra.show = true
            this.data.compra.editar = false
            this.data.compra.title = 'NUEVA ORDEN DE COMPRA'
        },

        formatedCurrency(key) {
            const value = formatters.formatCurrency(key)
            return value
        },

        formatedDate(dataString) {
            const value = formatters.formatDate(dataString)
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
            const result = await this.data.requestHttp.deleteCompra(this.data.selectedItem.idCompra)
            if (result !== null) {
                alert('Orden Eliminada')
                this.getOrdenes()
            } else {
                alert('No se pudo eliminar el registro')
            }
        },

        closeDialog(val) {  
           this.data.compra.show = val
           this.data.viewOrden.show = val
           this.data.compra.editar = val
           this.data.compra.title = ''
        }

    }
}
</script>

<style scoped>
.font{
    font-size: 12px !important;
}
</style>