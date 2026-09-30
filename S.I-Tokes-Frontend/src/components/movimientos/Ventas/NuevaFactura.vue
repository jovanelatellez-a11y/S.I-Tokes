<template>
    <v-dialog v-model="localShow" max-width="800" persistent>
        <v-card id="diag-fact">
            <v-card-title class="bg-red-darken-4 d-flex align-center">
                <h5><v-icon>mdi-file-document-outline</v-icon>{{ localTitle }}</h5>
                <v-spacer />
                <v-btn icon size="small" color="white" variant="tonal" @click="closeDialog()">
                    <v-icon>mdi-close</v-icon>
                    <v-tooltip activator="parent" location="top" text="Cerrar" />
                </v-btn>
            </v-card-title>
            <v-divider />
            <v-card-text id="body-card" class="">
                <v-row class="pb-0">
                    <v-col cols="6" md="4" sm="6" class="d-flex justify-start align-center pb-0">
                        <small class="mr-2">Nº Factura: </small>
                        <strong>{{ data.venta.noVenta }}</strong>
                    </v-col>
                    <v-col cols="6" md="4" sm="3" class="d-flex justify-start align-center pb-0">
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">Emision: </small>
                            <small><strong>{{ localEdit ? formatedDate(data.editVenta.fechaRegistro) : formatedDate(data.emision) }}</strong></small>
                        </div>
                    </v-col>
                    <v-col cols="12" md="4" sm="3" class="d-flex justify-start align-center pb-0">
                        <div class="d-flex align-center">
                            <v-checkbox v-model="data.nio" color="indigo" density="compact" class="label" hide-details>
                                <template v-slot:label>
                                    <span id="checkLabel">Córdobas</span>
                                </template>
                            </v-checkbox>
                            <v-checkbox v-model="data.usd" color="indigo" density="compact" class="label" hide-details>
                                <template v-slot:label>
                                    <span id="checkLabel">Dólares</span>
                                </template>
                            </v-checkbox>
                            <v-checkbox v-model="data.venta.credito" color="indigo" density="compact" class="label" hide-details>
                                <template v-slot:label>
                                    <span id="checkLabel">Crédito</span>
                                </template>
                            </v-checkbox>
                        </div>
                    </v-col>
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center my-4">
                    <small class="mx-6 font-weight-bold">GENERALES</small>
                    <v-divider/>
                </v-card-subtitle>
                <v-row>
                    <v-col cols="12" md="4" sm="12">
                        <v-text-field v-model="data.venta.noVenta" prepend-inner-icon="mdi-shopping" density="compact" variant="outlined" hide-details 
                            label="Nº Factura" placeholder="nº factura" persistent-placeholder/>
                    </v-col>
                    <v-col cols="12" md="4" sm="12">
                        <v-autocomplete v-model="data.venta.idCliente" prepend-inner-icon="mdi-account" density="compact" variant="outlined" hide-details label="Cliente" 
                            placeholder="ingrese el nombre del cliente" persistent-placeholder :items="data.clientes"/>
                    </v-col>
                    <v-col cols="12" md="4" sm="12">
                        <v-text-field v-model="data.venta.usuarioRegistro" prepend-inner-icon="mdi-account-cog" density="compact" variant="outlined" hide-details 
                            label="Empleado" placeholder="empleado de registro" persistent-placeholder/>
                    </v-col>
                    <v-col cols="12" md="12" sm="12">
                        <v-textarea v-model="data.venta.enviarA" density="compact" variant="outlined" hide-details label="Dirección de envio" placeholder="ingrese una dirección" 
                            persistent-placeholder rows="2"/>                        
                    </v-col>
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center my-4">                    
                    <small class="mx-6 font-weight-bold">PRODUCTOS</small>
                    <v-divider/>
                </v-card-subtitle>
                <v-row>
                    <v-col cols="12" md="4" sm="6">
                        <v-autocomplete v-model="data.producto.idProducto" prepend-inner-icon="mdi-shopping" density="compact" variant="outlined" hide-details 
                            label="Productos" placeholder="productos a agregar" persistent-placeholder :items="data.productos"/>
                    </v-col>
                    <v-col cols="6" md="4" sm="6">
                        <v-text-field v-model="data.producto.cantidad" prepend-inner-icon="mdi-numeric" density="compact" variant="outlined" hide-details 
                            label="Cantidad" placeholder="cantidad de productos" persistent-placeholder type="number"/>
                    </v-col>
                    <v-col cols="6" md="4" sm="6" class="d-flex justify-end align-center py-0">
                        <v-btn icon class="border mr-2" color="grey" size="small" variant="text" @click="clearProductos()">
                            <v-icon>mdi-broom</v-icon>
                            <v-tooltip activator="parent" location="bottom">Limpiar</v-tooltip>
                        </v-btn>

                        <v-btn icon color="red-darken-4" size="small" variant="tonal" @click="addProducts()">
                            <v-icon>mdi-plus</v-icon>
                            <v-tooltip activator="parent" location="bottom">Agregar Producto</v-tooltip>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" sm="12" md="12">
                        <v-data-table class="border rounded font" density="compact" :headers="data.headers" :items="data.items">
                            <template v-slot:item.opc="{ item }">
                                <v-tooltip text="Eliminar" location="top">
                                    <template v-slot:activator="{ props }">
                                        <v-icon v-bind="props" color="error" @click="deleteProduct(item)" class="mr-1">mdi-delete</v-icon>
                                    </template>
                                </v-tooltip>
                            </template>
                            <template v-slot:item.costoUnitario="{ item }">
                                <div>{{ formatedCurrency(item.costoUnitario, data.fornates.nio) }}</div>
                            </template>
                            <template v-slot:item.subTotal="{ item }">
                                <div>{{ formatedCurrency(item.subTotal, data.fornates.nio) }}</div>
                            </template>
                        </v-data-table>
                    </v-col>
                    <v-col cols="12" md="6" sm="6">
                        <v-textarea v-model="data.venta.observaciones" density="compact" variant="outlined" hide-details label="Observaciones" placeholder="ingrese algunos detalles de la factura" 
                            persistent-placeholder rows="3"/>                        
                    </v-col>
                    <v-col cols="12" md="6" sm="6" class="d-flex flex-column justify-end align-end">
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">Sub Total: </small>
                            <strong>{{ formatedCurrency(data.factura.subTotal, data.fornates.nio) }}</strong>
                        </div>
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">Total: </small>
                            <strong>{{ formatedCurrency(data.factura.total, data.fornates.nio) }}</strong>
                        </div>
                        <div v-if="data.usd" class="d-flex justify-end align-center">
                            <small class="mr-2">TOTAL $: </small>
                            <strong>{{ formatedCurrency(data.factura.usdTotal, data.fornates.usd) }}</strong>
                        </div>
                    </v-col>
                </v-row>
            </v-card-text>

            <v-divider/>
            <v-card-actions>
                <v-btn color="grey" variant="outlined" @click="closeDialog()">
                    Cancelar
                </v-btn>
                <v-btn class="bg-red-darken-4" @click="guardarFactura()">
                    Guardar
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
import { formatters } from '@/helpers/formatters';
import RequestHttp from '@/services/requestHttp';
import { reactive, ref, watch } from 'vue';

export default {
    mounted() {
        this.getClientes()
        this.getProductos()
    },

    props: {
        show: {
            type: Boolean,
            required: true,
        },
        editar: {
            type: Boolean,
            required: false
        },
        idFact: {
            type: Number,
            required: false
        },
        title: {
            type: String,
            required: true,
            default: 'Nueva Factura'
        }
    },

    setup(props) {
        const getVenta = async (id) => {
            return await data.requestHttp.getByIdVenta(id)
        }
        const calcularFactura = () => {
            let subtotal = 0
            data.factura.subTotal = 0.00
            data.factura.total = 0.00
            data.factura.usdTotal = 0.00
            data.items.forEach(item => {
                item.subTotal = item.costoUnitario * item.cantidad
                subtotal += item.subTotal

            })
            data.factura.subTotal = subtotal
            data.factura.total = data.factura.subTotal
            data.factura.usdTotal = data.factura.total / 36.6243
        }
        const localShow = ref(props.show)
        const localEdit = ref(props.editar)
        const localFact = ref(props.idFact)
        const localTitle = ref(props.title)
        watch(() => props.show, (newValue) => {
            localShow.value = newValue
        })
        watch(() => props.editar, async (val) => {
            localEdit.value = val
            if (val === true) {
                const result = await getVenta(localFact.value)
                data.venta.credito = result.credito
                data.venta.enviarA = result.enviarA
                data.venta.idCliente = result.idCliente
                data.venta.noVenta = result.noVenta
                data.venta.observaciones = result.observaciones
                data.venta.usuarioRegistro = result.usuarioRegistro
                data.editVenta.estado = result.estado
                data.editVenta.fechaRegistro = result.fechaRegistro
                data.editVenta.idVenta = result.idVenta
                data.editVenta.idClienteNavigation = result.idClienteNavigation
                result.detalleCxcs.map(item => {
                    data.editVenta.detalleCxcs.push(item)
                })
                const promises = result.detalleVenta.map(async (item) => {
                    const product = await data.requestHttp.getByIdProducto(item.idProducto)
                    data.items.push({
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
                calcularFactura()
            }
        })
        watch(() => props.idFact, async (val) => {
            localFact.value = val
        })
        watch(() => props.title, (val) => {
            localTitle.value = val
        })

        const data = reactive({
            headers: [
                {title: '', key: 'opc', align: 'center'},
                {title: 'Producto', key: 'producto', align: 'center'},
                {title: 'Cantidad', key: 'cantidad', align: 'center'},
                {title: 'costoUnitario Unit.', key: 'costoUnitario', align: 'center'},
                {title: 'SubTotal', key: 'subTotal', align: 'center'},
            ],
            productos: [],
            items: [],
            clientes: [],
            producto: {
                idVenta: 0,
                idProducto: null, 
                cantidad: 0,
                costoUnitario: null,
                observaciones: null,
            },
            tipoProductos: [
                {title: 'Camisas', key: 'Camisas'},
                {title: 'Pantalones', key: 'pantalones'},
            ],
            factura: {
                subTotal: 0.00,
                total: 0.00,
                usdTotal: 0.00
            },
            venta: {
                noVenta: null,
                idCliente: null,
                credito: false,
                observaciones: null,
                enviarA: null,
                usuarioRegistro: null,
                detalleVenta: []
            },
            editVenta: {
                idVenta: null,
                fechaRegistro: null,
                estado: false,
                idClienteNavigation: null,
                detalleCxcs: [],
            },

            idVenta: null,
            emision: new Date(),
            nio: true,
            usd: false,
            fornates: {
                nio: 'NIO', 
                usd: 'USD'
            },
            requestHttp: new RequestHttp()
        })

        return {
            localShow,
            localEdit,
            localFact,
            localTitle,
            data
        }
    },

    methods: {
        async getClientes() {
            this.data.clientes = []
            this.data.loading = true
            const result = await this.data.requestHttp.getClientes()
            this.data.loading = false
            result.map(item => {
                this.data.clientes.push({title: item.codigo, value: item.idCliente})
            })
        },

        async getProductos() {
            this.data.productos = []
            this.data.loading = true
            const result = await this.data.requestHttp.getProductos()
            this.data.loading = false

            if (result !== null) {
                result.map(item => {
                    this.data.productos.push({title: item.nombre, value: item.idProducto})
                })
            } else {
                throw new Error('Error en la solicitud')
            }
        },

        async addProducts() {
            if (!this.data.producto.idProducto || !this.data.producto.cantidad) {
                alert('Ingrese la informacion de producto')
                return
            }
            this.data.producto.cantidad = Number(this.data.producto.cantidad)
            const product = await this.data.requestHttp.getByIdProducto(this.data.producto.idProducto)
            this.data.items.push({
                ...this.data.producto,
                producto: product.nombre,
                costoUnitario: product.precio,
                subTotal:  this.data.producto.cantidad * product.costoUnitario
            })
            alert('Producto Agregado')
            this.calcularFactura()
            this.data.producto.idProducto = null
            this.data.producto.cantidad = 0
            this.data.producto.observaciones = null
        },
        
        deleteProduct(itemSelected) {
            const items = this.data.items.filter(item => item.idProducto !== itemSelected.idProducto)
            this.data.items = []
            items.map(item => {
                this.data.items.push(item)
            })
            this.calcularFactura()       
        },

        async guardarFactura() {
            this.data.venta.detalleVenta = []
            if (!this.localEdit) {
                if (!this.data.venta.noVenta ||
                    !this.data.venta.idCliente ||
                    !this.data.venta.enviarA ||
                    !this.data.venta.usuarioRegistro
                ) {
                    alert('Complete la informacion de la orden')
                    return
                } else {
                    this.data.items.forEach(item => {
                        this.data.venta.detalleVenta.push({
                            "idVenta": item.idVenta,
                            "idProducto": item.idProducto,
                            "cantidad": item.cantidad,
                            "precioUnitario": item.costoUnitario,
                            "observaciones": item.observaciones
                        })
                    })
                    const result = await this.data.requestHttp.postVenta(this.data.venta)

                    if (result !== null) {
                        alert('Registro Guardado')
                        this.closeDialog()
                    } else {
                        alert('No se pudo guardar la orden')
                        return
                    }
                    
                }
            } else {
                if (!this.data.venta.noVenta ||
                    !this.data.venta.idCliente ||
                    !this.data.venta.enviarA ||
                    !this.data.venta.usuarioRegistro
                ) {
                    alert('Complete la informacion de la orden')
                    return
                } else {
                    this.data.items.forEach(item => {
                        this.data.venta.detalleVenta.push({
                            "idVenta": item.idVenta,
                            "idProducto": item.idProducto,
                            "cantidad": item.cantidad,
                            "precioUnitario": item.costoUnitario,
                            "observaciones": item.observaciones
                        })
                    })

                    const result = await this.data.requestHttp.putVenta(this.data.venta, this.data.editVenta.idVenta)

                    if (result !== null) {
                        alert('Registro Editado')
                        this.closeDialog()
                    } else {
                        alert('Registro Editado')
                        this.closeDialog()
                        return
                    }
                }
            }
            this.$emit('refreshTable')
        },

        formatedCurrency(key, currency) {
            const value = formatters.formatCurrency(key, currency)
            return value
        },

        formatedDate(dataString) {
            const value = formatters.formatDate(dataString)
            return value
        },

        closeDialog() {
            this.$emit('closeDialog', false)
            this.data.items = []
            this.data.factura.subTotal = 0.00
            this.data.factura.total = 0.00
            this.data.factura.usdTotal = 0.00
            this.data.venta = {}
            this.data.editVenta = {}

            this.clearProductos()
        },

        clearProductos() {
            this.data.producto.cantidad = 0.00
            this.data.producto.idProducto = null
        },

        calcularFactura() {
            let subtotal = 0
            this.data.factura.subTotal = 0.00
            this.data.factura.total = 0.00
            this.data.factura.usdTotal = 0.00
            this.data.items.forEach(item => {
                item.subTotal = item.costoUnitario * item.cantidad
                subtotal += item.subTotal

            })
            this.data.factura.subTotal = subtotal
            this.data.factura.total = this.data.factura.subTotal
            this.data.factura.usdTotal = this.data.factura.total / 36.6243
        }
    },
}
</script>

<style scoped>
.v-card-item{
    padding: 8px 12px !important;
}

#diag-fact{
    position: relative;
}

#body-card{
    z-index: 2;
}

#checkLabel{
    font-size: 12px !important;
}

.font{
    font-size: 12px !important;
}
</style>