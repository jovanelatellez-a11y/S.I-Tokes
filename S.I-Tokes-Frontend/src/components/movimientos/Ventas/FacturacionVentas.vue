<template>
    <div class="w-100">
        <v-card class="border" elevation="0" rounded="0">
            <!-- Encabezado -->
            <template v-slot:prepend>
                <div class="d-flex align-center">
                    <!-- Título -->
                    <div class="text-h6 font-weight-bold d-flex align-center">
                    <v-icon class="me-2" color="red-darken-4">mdi-package-variant</v-icon>
                        Facturación
                    </div>
                </div>
            </template>
            <template v-slot:append>
                <v-btn class="bg-red-darken-4 rounded-" @click="createFactura()">
                    <v-icon>mdi-plus</v-icon>
                    <v-tooltip activator="parent" location="left">Nueva Factura</v-tooltip> 
                </v-btn>
            </template>
            <v-divider /> 
            <v-row class="pa-2" dense>
                <v-col cols="6" md="6" sm="6">
                    <v-text-field v-model="data.search" color="red-darken-4" density="compact" variant="outlined" append-inner-icon="mdi-magnify" label="Buscar productos"
                        hide-details placeholder="Ingrese un texto a buscar..." persistent-placeholder/>
                </v-col>
                <v-col cols="6" md="6" sm="6" class="d-flex justify-end align-center">
                    <v-btn icon color="green" size="small" variant="text" class="mr-2 border" @click="getVentas()">
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
                <v-data-table :search="data.search" :mobile="isMobile" :headers="data.header" :items="data.facturas" class="border" density="compact">
                    <template v-slot:item.total="{ item }">
                        <div>{{ formatedCurrency(item.total) }}</div>
                    </template>
                    <template v-slot:item.fechaRegistro="{ item }">
                        <div>{{ formatedDate(item.fechaRegistro) }}</div>
                    </template>
                    <template v-slot:item.opc="{ item }">
                        <v-tooltip text="Editar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="green" @click="editFactura(item)" class="mr-1" >mdi-pencil</v-icon>
                            </template>
                        </v-tooltip>
                        
                        <v-tooltip text="Eliminar" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="error" class="mr-1" @click="showAlert(item)">mdi-delete</v-icon>
                            </template>
                        </v-tooltip>

                        <v-tooltip text="Ver" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="viewFactura(item)">mdi-eye</v-icon>
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
        <NuevaFactura :show="data.editFactura.show" :editar="data.editFactura.editar" :idFact="data.idVenta"
            @closeDialog="closeDialog" :title="data.editFactura.title" @refreshTable="getVentas"/>
        <ViewVenta :show="data.viewFactura.show" :factura="data.viewFactura.item" @closeDialog="closeDialog"/>
        <AlertComp :show="data.viewAlert" @deleteItem="deleteAction"/>
    </div>
</template>

<script>
import { formatters } from '@/helpers/formatters.js';
import { reactive, computed, ref, onMounted, onUnmounted } from 'vue';
import NuevaFactura from './NuevaFactura.vue';
import ViewVenta from './ViewVenta.vue';
import RequestHttp from '@/services/requestHttp';
import AlertComp from '@/components/widgets/AlertComp.vue';

export default {
    mounted() {
        this.getVentas()
    },

    components: {
        NuevaFactura,
        ViewVenta,
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

        const dateHastaFormatted = ref(null)
        const dateDesdeFormatted = ref(null)
        const dateHasta = computed(() => {
            return dateHastaFormatted.value ? new Date(dateHastaFormatted.value).toLocaleDateString() : null;
        })

        const dateDesde = computed(() => {
            return dateDesdeFormatted.value ? new Date(dateDesdeFormatted.value).toLocaleDateString() : null;
        })
        const data = reactive({
            header: [
                {title: '', key: 'opc', align: 'center',},
                {title: 'Nº Factura', key: 'noVenta', align: 'center'},
                {title: 'Cliente', key: 'cliente', align: 'center'},
                {title: 'Vendedor', key: 'usuarioRegistro', align: 'center'},
                {title: 'Total', key: 'total', align: 'center'},
                {title: 'Dirección', key: 'enviarA', align: 'center'},
                {title: 'Fecha Emisión', key: 'fechaRegistro', align: 'center'},
                {title: 'Observaciones', key: 'observaciones', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            facturas: [],
            visibleDialog: false,

            editFactura: {
                show: false,
                item: {},
                editar: false,
                title: ''
            },
            viewFactura: {
                show: false,
                item: {}
            },
            search: null,
            viewAlert: false,
            selectedItem: null,
            idVenta: null,
            menuDesde: false,
            menuHasta: false,
            requestHttp: new RequestHttp()
        })

        return {
            data,
            dateDesde,
            dateDesdeFormatted,
            dateHasta,
            dateHastaFormatted,
            isMobile
        }
    },

    methods: {
        async getVentas() {
            this.data.facturas = []
            this.data.loading = true
            const result = await this.data.requestHttp.getVentas()
            this.data.loading = false
            if (result !== null) {
                result.map(item => {
                    this.data.facturas.push(item)
                })
            }
        },
        formatedCurrency(key) {
            const value = formatters.formatCurrency(key)
            return value
        },
        
        formatedDate(dataString) {
            const value = formatters.formatDate(dataString)
            return value
        },
        
        getStatusColor(status) {
            const statusColors = {
                'Activo': 'success',
                'Inactivo': 'warning',
                'Descontinuado': 'error'
            }
            return statusColors[status] || 'grey'
        },
        
        createFactura() {
            this.data.editFactura.title = ''
            this.data.editFactura.show = true
            this.data.editFactura.editar = false
            this.data.editFactura.title = 'NUEVA FACTURA'
        },
        
        viewFactura(item) {
            this.data.viewFactura.show = true
            this.data.viewFactura.item = item
        },
        
        editFactura(item) {
            this.data.editFactura.show = true
            this.data.editFactura.editar = true
            this.data.idVenta = item.idVenta
            this.data.editFactura.title = 'EDITAR FACTURA'
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
            const result = await this.data.requestHttp.deleteVenta(this.data.selectedItem.idVenta)
            if (result !== null) {
                alert('Venta Eliminada')
                this.getVentas()
            } else {
                alert('No se pudo eliminar el registro')
            }
        },

        closeDialog(val) {  
            this.data.viewFactura.show = val
            this.data.editFactura.show = val
            this.data.editFactura.editar = val
            this.data.editFactura.show = val
            this.data.editFactura.title = ''
        }

    }
}
</script>

<style scoped>

</style>