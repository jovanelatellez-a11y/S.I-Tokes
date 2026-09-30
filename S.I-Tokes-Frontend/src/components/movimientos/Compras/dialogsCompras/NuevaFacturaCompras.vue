<template>
    <v-dialog v-model="localShow" max-width="850" persistent>
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
                    <v-col cols="6" md="4" sm="4" class="d-flex justify-start align-center pb-0">
                        <small class="mr-2">Nº Órden: </small>
                        <strong>{{ data.orden.noOrden }}</strong>
                    </v-col>
                    <v-col v-if="!localEdit" cols="6" md="4" sm="4" class="d-flex justify-start align-center pb-0">
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">Emision: </small>
                            <small><strong>{{ localEdit ? '' : formatedDate(data.nowDate) }}</strong></small>
                        </div>
                    </v-col>
                    <v-col cols="12" md="4" sm="4" class="d-flex justify-space-around align-center pb-0">
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
                            <v-checkbox v-model="data.orden.aprobada" color="indigo" density="compact" class="label" hide-details>
                                <template v-slot:label>
                                    <span id="checkLabel">Aprobada</span>
                                </template>
                            </v-checkbox>
                        </div>
                    </v-col>
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center my-4">
                    <small class="mx-6 font-weight-bold">GENERALES</small>
                    <v-divider/>
                </v-card-subtitle>
                <v-row dense>
                    <v-col cols="12" md="4" sm="6" class="py-1">
                        <v-text-field v-model="data.orden.noOrden" prepend-inner-icon="mdi-file-document" density="compact" variant="outlined" hide-details label="Num. Órden" placeholder="número de orden" 
                            persistent-placeholder/>
                    </v-col>
                    <v-col cols="12" md="4" sm="6" class="py-1">
                        <v-autocomplete v-model="data.orden.idProveedor" prepend-inner-icon="mdi-account" density="compact" variant="outlined" hide-details label="Proveedor" placeholder="ingrese el proveedor" 
                            persistent-placeholder :items="data.proveedores"/>
                    </v-col>
                    <v-col cols="12" md="4" sm="6" class="py-1">
                        <v-text-field v-model="data.orden.usuarioRegistro" prepend-inner-icon="mdi-account-cog" density="compact" variant="outlined" hide-details label="Empleado" placeholder="empleado de registro" 
                            persistent-placeholder/>
                    </v-col>
                </v-row>

                <v-card-subtitle class="d-flex align-center text-center my-4">                    
                    <small class="mx-6 font-weight-bold">PRODUCTOS</small>
                    <v-divider/>
                </v-card-subtitle>

                <v-row dense>
                    <v-col cols="12" md="4" sm="6" class="py-1">
                        <v-autocomplete v-model="data.producto.idProducto" prepend-inner-icon="mdi-shopping" density="compact" variant="outlined" hide-details label="Productos" placeholder="productos a agregar" 
                            persistent-placeholder :items="data.productos"/>
                    </v-col>
                    <v-col cols="12" md="4" sm="6" class="py-1">
                        <v-text-field v-model="data.producto.cantidad" prepend-inner-icon="mdi-numeric" density="compact" variant="outlined" hide-details label="Cantidad" paceholder="cantidad de productos" 
                            persistent-placeholder type="number"/>
                    </v-col>
                    <v-col cols="12" md="4" sm="12" class="d-flex justify-end align-center py-0">
                        <v-btn icon color="red-darken-4" size="small" variant="tonal" @click="addProducts">
                            <v-icon>mdi-plus</v-icon>
                            <v-tooltip activator="parent" location="bottom">Agregar Producto</v-tooltip>
                        </v-btn>
                    </v-col>
                </v-row>

                <v-card-subtitle class="d-flex align-center text-center my-4">                    
                    <small class="mx-6 font-weight-bold">DETALLES</small>
                    <v-divider/>
                </v-card-subtitle>
                <v-row>
                    <v-col cols="12" sm="12" md="12">
                        <v-data-table class="border rounded" density="compact" :headers="data.headers" :items="data.items">
                            <template v-slot:item.opc="{ item }">
                                <v-tooltip text="Eliminar" location="top">
                                    <template v-slot:activator="{ props }">
                                        <v-icon v-bind="props" color="error" @click="deleteProduct(item)" class="mr-1">mdi-delete</v-icon>
                                    </template>
                                </v-tooltip>
                            </template>
                            <template v-slot:item.costoUnitario="{ item }">
                                <div>{{ formatedCurrency(item.costoUnitario) }}</div>
                            </template>
                            <template v-slot:item.subTotal="{ item }">
                                <div>{{ formatedCurrency(item.subTotal) }}</div>
                            </template>
                        </v-data-table>
                    </v-col>
                    <v-col cols="12" md="6" sm="6">
                        <v-textarea v-model="data.orden.observaciones" density="compact" variant="outlined" hide-details label="Observaciones" placeholder="ingrese algunos detalles de la factura" 
                            persistent-placeholder rows="3"/>                        
                    </v-col>
                    <v-col cols="12" md="6" sm="6" class="d-flex flex-column justify-end align-end">
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">Sub Total: </small>
                            <strong>{{ formatedCurrency(data.factura.subTotal, data.fomates.nio) }}</strong>
                        </div>
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">TOTAL: </small>
                            <strong>{{ formatedCurrency(data.factura.total, data.fomates.nio) }}</strong>
                        </div>
                        <div v-if="data.usd" class="d-flex justify-end align-center">
                            <small class="mr-2">TOTAL $: </small>
                            <strong>{{ formatedCurrency(data.factura.usdTotal, data.fomates.usd) }}</strong>
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

    props: {
        show: {
            type: Boolean,
            required: true,
            default: true
        },
        editar: {
            type: Boolean,
            required: false
        },
        orden: {
            type: Object,
            required: false
        },
        title: {
            type: String,
            required: true,
            default: 'NUEVA ORDEN DE COMPRA'
        }
    },

    setup(props) {
        const getProveedores = async () =>  {
            data.proveedores = []
            const result = await data.requestHttp.getProveedores()
            result.map(item => {
                data.proveedores.push({title: item.nombre, value: item.idProveedor})
            })
        }

        const getProductos =  async () =>  {
            data.productos = []
            const result = await data.requestHttp.getProductos()
            result.map(item => {
                data.productos.push({title: item.nombre, value: item.idProducto})
            })
        }

        const localShow = ref(props.show)
        const localEdit = ref(props.editar)
        const localOrden = ref(props.orden)
        const localTitle = ref(props.orden)
        watch(() => props.show, (newValue) => {
            localShow.value = newValue
            if (newValue) {
                getProductos()
                getProveedores()
            }
        })
        watch(() => props.editar, async (val) => {
            localEdit.value = val
            if (val === true) {
                const result = await data.requestHttp.getByIdCompra(localOrden.value.idCompra)
                data.idOrden = result.idCompra
                data.orden.idProveedor = result.idProveedor
                data.orden.noOrden = result.noOrden
                data.orden.aprobada = result.aprobada
                data.orden.observaciones = result.observaciones
                data.orden.usuarioRegistro = result.usuarioRegistro
                data.producto.idCompra = localOrden.value.idCompra
                const promises = result.detalleCompras.map(async (item) => {
                    const product = await data.requestHttp.getByIdProducto(item.idProducto)
                    data.items.push({
                        idCompra: item.idCompra,
                        idProducto: item.idProducto, 
                        cantidad: item.cantidad,
                        costoUnitario: item.costoUnitario,
                        observaciones: item.observaciones,
                        subTotal: item.cantidad * item.costoUnitario,
                        producto: product.nombre
                    })
                })

                await Promise.all(promises)
            }
        })
        watch(() => props.orden, (val) => {
            localOrden.value = val
        })
        watch(() => props.title, (val) => {
            localTitle.value = val
        })


        const data = reactive({
            headers: [
                {title: '', key: 'opc', align: 'center'},
                {title: 'Producto', key: 'producto', align: 'center'},
                {title: 'Cantidad', key: 'cantidad', align: 'center'},
                {title: 'Precio Unit.', key: 'costoUnitario', align: 'center'},
                {title: 'SubTotal', key: 'subTotal', align: 'center'},
            ],
            items: [],
            proveedores: [],
            productos: [],
            producto: {
                idCompra: 0,
                idProducto: null, 
                cantidad: 0,
                costoUnitario: null,
                observaciones: null,
            },
            factura: {
                subTotal: 0.00,
                total: 0.00,
                usdTotal: 0.00
            },
            orden: {
                noOrden: null,
                idProveedor: null,
                aprobada: false,
                observaciones: null,
                usuarioRegistro: null,
                detalle: []
            },

            radioBtn: [
                {label: 'Al recibir productos', value: 'recibir'},
                {label: 'Inmediatamente', value: 'inmediato'},
            ],
            idOrden: null,
            nio: true,
            usd: false,
            impt: false,
            nowDate: new Date(),
            fomates: {
                nio: 'NIO', 
                usd: 'USD'
            },
            requestHttp: new RequestHttp()
        })

        return {
            localShow,
            localEdit,
            localTitle,
            localOrden,
            data
        }
    },

    methods: {
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
                subTotal:  this.data.producto.cantidad * product.precio
            })
            alert('Producto Agregado')
            this.calcularTotals()
            this.data.producto.idProducto = null
            this.data.producto.cantidad = 0
            this.data.producto.observaciones = null
        },

        async guardarFactura() {
            if (!this.localEdit) {
                if (!this.data.orden.noOrden ||
                    !this.data.orden.idProveedor ||
                    !this.data.orden.usuarioRegistro
                ) {
                    alert('Complete la informacion de la orden')
                    return
                } else {
                    this.data.items.forEach(item => {
                        this.data.orden.detalle.push({
                            "idCompra": item.idCompra,
                            "idProducto": item.idProducto,
                            "cantidad": item.cantidad,
                            "costoUnitario": item.costoUnitario,
                            "observaciones": item.observaciones
                        })
                    })
                    const result = await this.data.requestHttp.postCompra(this.data.orden)

                    if (result !== null) {
                        alert('Registro Guardado')
                        this.closeDialog()
                    } else {
                        alert('No se pudo guardar la orden')
                        return
                    }
                    
                }
            } else {
                if (!this.data.orden.noOrden ||
                    !this.data.orden.idProveedor ||
                    !this.data.orden.usuarioRegistro
                ) {
                    alert('Complete la informacion de la orden')
                    return
                } else {
                    this.data.items.forEach(item => {
                        this.data.orden.detalle.push({
                            "idCompra": item.idCompra,
                            "idProducto": item.idProducto,
                            "cantidad": item.cantidad,
                            "costoUnitario": item.costoUnitario,
                            "observaciones": item.observaciones
                        })
                    })

                    const result = await this.data.requestHttp.putCompra(this.data.orden, this.data.idOrden)

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

        calcularTotals() {
            let subTotal = 0
            this.data.factura.subTotal = 0
            this.data.factura.total = 0
            this.data.factura.usdTotal = 0

            this.data.items.forEach(item => {
                subTotal += item.subTotal
            })

            this.data.factura.subTotal = subTotal
            this.data.factura.total = this.data.factura.subTotal
            this.data.factura.usdTotal = this.data.factura.total/36.4263
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
            this.data.factura.subTotal = 0
            this.data.factura.total = 0
            this.data.factura.usdTotal = 0
            this.data.orden.aprobada = false
            this.data.orden.detalle = []
            this.data.items = []
            this.data.orden.idProveedor = null
            this.data.orden.noOrden = null
            this.data.orden.observaciones = null
            this.data.orden.usuarioRegistro = null
        },

        deleteProduct(itemSelected) {
            const items = this.data.items.filter(item => item.idProducto !== itemSelected.idProducto)
            this.data.items = []
            items.map(item => {
                this.data.items.push(item)
            })
            this.calcularTotals()          
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

</style>

