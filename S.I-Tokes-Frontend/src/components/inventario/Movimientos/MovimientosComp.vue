<template>
    <div>
        <v-card class="border" elevation="0" rounded="0">
            <template v-slot:prepend>
            <div class="d-flex align-center">
                <!-- Título -->
                    <div class="font-weight-bold d-flex align-center">
                        <v-icon class="me-2" color="red-darken-4">mdi-warehouse</v-icon>
                        <span class="text-h6 font-weight-bold">Movimientos</span>
                    </div>
                </div>
            </template>
            <v-divider /> 
            <v-card-text class="px-0">
                <v-row dense class="align-center px-2">
                    <v-col cols="12" sm="6" md="3">
                        <v-text-field v-model="data.search" density="compact" variant="outlined" label="Buscar" hide-details 
                            placeholder="Buscar textos" persistent-placeholder/>
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                        <v-autocomplete v-model="data.movimiento" :items="data.movimientos" density="compact" variant="outlined" label="Tipos de Movimientos" hide-details 
                            placeholder="movimientos" persistent-placeholder/>
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                        <v-autocomplete v-model="data.producto" :items="data.products" density="compact" variant="outlined" label="Producto" hide-details placeholder="productos" persistent-placeholder/>
                    </v-col>
                    <v-col cols="12" sm="6" md="3" class="d-flex justify-end align-center">
                        <v-btn icon color="red-darken-4" class="border" variant="text">
                            <v-icon>mdi-magnify</v-icon>
                        </v-btn>
                        <v-btn icon color="green" class="border mx-2" variant="text">
                            <v-icon>mdi-magnify</v-icon>
                        </v-btn>
                        <v-btn icon color="grey" variant="text" class="border">
                            <v-icon>mdi-broom</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-divider class="my-2"/> 
                <v-card-subtitle class="d-flex align-center text-center mb-2">
                    <v-divider /> 
                    <span class="mx-6 text-grey font-weight-bold">Registros</span>
                    <v-divider />
                </v-card-subtitle>
                <v-data-table :search="data.search" :headers="data.headers" :items="data.items" class="border font" density="compact">
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
                        <v-tooltip text="Ver" location="top">
                            <template v-slot:activator="{ props }">
                                <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="viewDialog(item)">mdi-eye</v-icon>
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
        <v-dialog v-model="data.dialog" max-width="700">
            <v-card class="rounded-lg">
                <!-- Encabezado -->
                <v-card-title class="d-flex justify-space-between align-center bg-red-darken-4">
                    <span class="text-h5 font-weight-bold text-white">Detalle del Movimiento</span>
                    <v-btn icon @click="data.dialog = false" variant="text" color="white">
                        <v-icon>mdi-close</v-icon>
                    </v-btn>
                </v-card-title>
                
                <v-divider />
                
                <v-card-text class="pa-2">
                    <!-- Sección de información principal -->
                    <v-row class="mb-4">
                        <v-col cols="12" md="6" class="py-1">
                            <div class="d-flex align-center mb-2">
                                <v-icon color="red-darken-4" class="me-3">mdi-numeric</v-icon>
                                <div>
                                    <div class="text-caption text-grey-darken-1">Nº Movimiento</div>
                                    <div class="font-weight-medium">{{ selectedItem.noMov }}</div>
                                </div>
                            </div>
                        </v-col>

                        <v-col cols="12" md="6" class="py-1">
                            <div class="d-flex align-center mb-2">
                                <v-icon color="red-darken-4" class="me-3">mdi-shape</v-icon>
                                <div>
                                    <div class="text-caption text-grey-darken-1">Tipo</div>
                                    <div class="font-weight-medium">{{ selectedItem.tipoMov }}</div>
                                </div>
                            </div>
                        </v-col>

                        <v-col cols="12" md="6" class="py-1">
                            <div class="d-flex align-center mb-2">
                                <v-icon color="red-darken-4" class="me-3">mdi-account</v-icon>
                                <div>
                                    <div class="text-caption text-grey-darken-1">Cliente/Proveedor</div>
                                    <div class="font-weight-medium">{{ selectedItem.clienteProveedor }}</div>
                                </div>
                            </div>
                        </v-col>

                        <v-col cols="12" md="6" class="py-1">
                            <div class="d-flex align-center mb-2">
                                <v-icon color="red-darken-4" class="me-3">mdi-cash</v-icon>
                                <div>
                                    <div class="text-caption text-grey-darken-1">Monto</div>
                                    <div class="font-weight-medium">{{ formatedCurrency(selectedItem.total) }}</div>
                                </div>
                            </div>
                        </v-col>

                        <v-col cols="12" md="6" class="py-1">
                            <div class="d-flex align-center mb-2">
                                <v-icon color="red-darken-4" class="me-3">mdi-calendar</v-icon>
                                <div>
                                    <div class="text-caption text-grey-darken-1">Fecha Registro</div>
                                    <div class="font-weight-medium">{{ formatedDate(selectedItem.fechaRegistro) }}</div>
                                </div>
                            </div>
                        </v-col>

                        <v-col cols="12" md="6" class="py-1">
                            <div class="d-flex align-center mb-2">
                                <v-icon color="red-darken-4" class="me-3">mdi-check-circle</v-icon>
                                <div>
                                    <div class="text-caption text-grey-darken-1">Aprobada</div>
                                    <div>
                                        <v-chip :color="selectedItem.aprobada ? 'success' : 'error'" size="small" class="font-weight-bold">
                                        {{ selectedItem.aprobada ? 'SI' : 'NO' }}
                                        </v-chip>
                                    </div>
                                </div>
                            </div>
                        </v-col>
                    </v-row>

                    <!-- Tabla de detalles -->
                    <v-row class="mb-2">
                        <v-col cols="12">
                            <v-data-table hide-default-footer class="border rounded" density="compact"
                                :headers="data.headersView" :items="data.itemsView" >
                            <template v-slot:item.costoUnitario="{ item }">
                                <div>{{ formatedCurrency(item.costoUnitario, data.fomates.nio) }}</div>
                            </template>
                            <template v-slot:item.subTotal="{ item }">
                                <div>{{ formatedCurrency(item.subTotal, data.fomates.nio) }}</div>
                            </template>
                            </v-data-table>
                        </v-col>
                    </v-row>

                    <!-- Sección inferior -->
                    <v-row>
                        <v-col cols="12" md="6">
                            <v-textarea v-model="data.orden.observaciones" density="compact" variant="plain" hide-details label="Observaciones" 
                                placeholder="Ingrese algunos detalles de la orden" persistent-placeholder rows="2" readonly class="custom-textarea"
                            />
                        </v-col>

                        <v-col cols="12" md="6">
                            <div class="d-flex justify-space-between align-center mb-2">
                                <span class="text-caption text-grey-darken-1">Sub Total:</span>
                                <strong class="font-weight-medium">{{ formatedCurrency(data.factura.subTotal, data.fomates.nio) }}</strong>
                            </div>
                            <div class="d-flex justify-space-between align-center mb-2">
                                <span class="text-caption text-grey-darken-1">Total:</span>
                                <strong class="font-weight-medium text-red-darken-4">{{ formatedCurrency(data.factura.total, data.fomates.nio) }}</strong>
                            </div>
                            <div class="d-flex justify-space-between align-center">
                                <span class="text-caption text-grey-darken-1">Total $:</span>
                                <strong class="font-weight-medium">{{ formatedCurrency(data.factura.usdTotal, data.fomates.usd) }}</strong>
                            </div>                    
                        </v-col>
                    </v-row>
                </v-card-text>
                
                <v-divider />
                
                <!-- Acciones -->
                <v-card-actions class="pa-2 justify-end bg-grey-lighten-4">
                    <v-btn  color="grey" variant="tonal" @click="closeDialog()" class="px-6" >
                        Cerrar
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>

<script>
import RequestHttp from '@/services/requestHttp';
import { formatters } from '@/helpers/formatters';
import { reactive, ref } from 'vue';

export default {
    mounted() {
        this.getData()
        this.getProductos()
    },

    setup() {
        const calcularTotals = () => {
            let subtotal = 0
            data.factura.subTotal = 0.00
            data.factura.total = 0.00
            data.factura.usdTotal = 0.00
            data.itemsView.forEach(item => {
                item.subTotal = item.costoUnitario * item.cantidad
                subtotal += item.subTotal

            })
            data.factura.subTotal = subtotal
            data.factura.total = data.factura.subTotal
            data.factura.usdTotal = data.factura.total / 36.6243
        }
        const getCliente = async (id) => {
            const result = await data.requestHttp.getByIdCliente(id)
            return result.codigo
        }
        const getVenta = async (id) => {
            data.itemsView = []
            const result = await data.requestHttp.getByIdVenta(id)

            data.venta.credito = result.credito
            data.venta.enviarA = result.enviarA
            data.venta.idCliente = result.idCliente
            data.venta.noVenta = result.noVenta
            data.venta.cliente = await getCliente(result.idCliente)
            data.venta.observaciones = result.observaciones
            data.venta.usuarioRegistro = result.usuarioRegistro
            data.venta.estado = result.estado
            data.venta.fechaRegistro = result.fechaRegistro
            data.venta.idVenta = result.idVenta
            data.venta.idClienteNavigation = result.idClienteNavigation
            result.detalleCxcs.map(item => {
                data.venta.detalleCxcs.push(item)
            })
            const promises = result.detalleVenta.map(async (item) => {
                const product = await data.requestHttp.getByIdProducto(item.idProducto)
                data.itemsView.push({
                    idDetalleVenta: item.idDetalleVenta,
                    idVenta: item.idVenta,
                    idProducto: item.idProducto,
                    cantidad: item.cantidad,
                    costoUnitario: item.precioUnitario,
                    observaciones: item.observaciones,
                    idProductoNavigation: item.idProductoNavigation,
                    idVentaNavigation: item.idVentaNavigation,
                    producto: product.nombre,
                    subTotal: item.cantidad * item.precioUnitario
                })
            })
            await Promise.all(promises)
            calcularTotals()   
        }

        const getCompra = async (id) => {
            const result = await data.requestHttp.getByIdCompra(id)
            const proveedor = await data.requestHttp.getByIdProveedor(result.idProveedor)

            data.itemsView = []
            data.orden.idOrden = result.idCompra
            data.orden.idProveedor = result.idProveedor
            data.orden.noOrden = result.noOrden
            data.orden.aprobada = result.aprobada
            data.orden.observaciones = result.observaciones
            data.orden.usuarioRegistro = result.usuarioRegistro
            data.orden.fechaRegistro = result.fechaRegistro
            data.orden.estado = result.estado
            data.orden.proveedor = proveedor.nombre
            await Promise.all(result.detalleCompras.map(async (item) => {
                const product = await data.requestHttp.getByIdProducto(item.idProducto)
                data.itemsView.push({
                    idCompra: item.idCompra,
                    idProducto: item.idProducto, 
                    cantidad: item.cantidad,
                    costoUnitario: item.costoUnitario,
                    observaciones: item.observaciones,
                    subTotal: item.cantidad * item.costoUnitario,
                    producto: product.nombre
                })
            }))
            calcularTotals()
        }

        const data = reactive({
            headers: [
                {title: '', key: 'opc', align: 'center',},
                {title: 'Nº Mov', key: 'noMov', align: 'center'},
                {title: 'Tipo Mov', key: 'tipoMov', align: 'center'},
                {title: 'Cliente-Proveedor', key: 'clienteProveedor', align: 'center'},
                {title: 'Vendedor', key: 'usuarioRegistro', align: 'center'},
                {title: 'Monto', key: 'total', align: 'center'},
                {title: 'FechaRegistro', key: 'fechaRegistro', align: 'center'},
                {title: 'Observaciones', key: 'observaciones', align: 'center'},
                {title: 'Estado', key: 'estado', align: 'center'},
            ],
            headersView: [
                {title: 'Producto', key: 'producto', align: 'center'},
                {title: 'Cantidad', key: 'cantidad', align: 'center'},
                {title: 'costoUnitario Unit.', key: 'costoUnitario', align: 'center'},
                {title: 'SubTotal', key: 'subTotal', align: 'center'},
            ],
            items: [],
            products: [],
            venta: {
                noVenta: null,
                idCliente: null,
                credito: false,
                cliente: null,
                observaciones: null,
                enviarA: null,
                usuarioRegistro: null,
                detalleVenta: [],
                idVenta: null,
                fechaRegistro: null,
                estado: false,
                idClienteNavigation: null,
                detalleCxcs: [],
            },
            orden: {
                idOrden: null,
                noOrden: null,
                idProveedor: null,
                proveedor: null,
                aprobada: false,
                observaciones: null,
                usuarioRegistro: null,
                fechaRegistro: null,
                estado: null,
                detalle: []
            },
            itemsView: [],
            factura: {
                subTotal: 0.00,
                total: 0.00,
                usdTotal: 0.00,
            },
            movimientos: [
                {title: 'Venta', value: 'Venta'},
                {title: 'Compra', value: 'Compra'},
            ],
            nio: true,
            usd: false,
            idOrden: null,
            fomates: {
                nio: 'NIO', 
                usd: 'USD'
            },
            movimiento: null,
            producto: null,
            dialog: false,
            search: null,
            requestHttp: new RequestHttp()
        })
        const selectedItem = ref({});

        return {
            data,
            selectedItem,
            getCompra,
            getVenta
        }
    },

    methods: {
        async getOrdenes() {            
            this.data.loading = true
            const result = await this.data.requestHttp.getCompras()
            this.data.loading = false
            if (result !== null) {
                result.map(item => {
                    this.data.items.push({
                        ...item,
                        clienteProveedor: item.proveedor,
                        tipoMov: 'Compra',
                        noMov: item.noOrden
                    })
                })
            }
        },

        async getVentas() {
            
            const result = await this.data.requestHttp.getVentas()
            if (result !== null) {
                result.map(item => {
                    this.data.items.push({
                        ...item,
                        clienteProveedor: item.cliente,
                        tipoMov: 'Venta',
                        noMov: item.noVenta
                    })
                })
            }
        },

        async getData () {
            this.data.items = []
            this.data.loading = true
            await Promise.all([
                this.getVentas(),
                this.getOrdenes()
            ])
            this.data.loading = true
        },

        async getProductos() {
            this.data.products = []
            const result = await this.data.requestHttp.getProductos()

            if (result !== null) {
                result.map(item => {
                    this.data.products.push({title: item.nombre, value: item.idProducto})
                })
            } else {
                throw new Error('Error en la solicitud')
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

        viewDialog(item) {
            if (item.tipoMov === 'Venta') {
                this.getVenta(item.idVenta)
            } else {
                this.getCompra(item.idCompra)
            }
            this.selectedItem = { ...item }
            this.data.dialog = true
        },

        closeDialog() {
            this.data.dialog = false
            this.data.itemsView = []
            this.data.factura.subTotal = 0.00
            this.data.factura.total = 0.00
            this.data.factura.usdTotal = 0.00
        }
    }

}
</script>

<style scoped>

</style>