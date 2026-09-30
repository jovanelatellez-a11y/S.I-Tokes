<template>
    <v-dialog v-model="localShow" max-width="600" persistent>
        <v-card id="diag-fact">
            <v-card-title class="bg-red-darken-4 d-flex align-center">
                <h5><v-icon>mdi-account-plus</v-icon>{{ localTitle }}</h5>
                <v-spacer />
                <v-btn icon size="small" color="white" variant="tonal" @click="closeDialog()">
                    <v-icon>mdi-close</v-icon>
                    <v-tooltip activator="parent" location="top" text="Cerrar" />
                </v-btn>
            </v-card-title>
            <v-divider />
            <v-card-text id="body-card" class="">
                <v-row v-if="!localEdit && !localView" class="pb-0">
                    <v-col cols="12" md="12" sm="12" class="d-flex justify-end align-center pb-0">
                        <div class="d-flex justify-end align-center">
                            <small class="mr-2">Fecha de Registro: </small>
                            <small><strong>{{ localEdit ? '' : formatedDate(data.nowDate) }}</strong></small>
                        </div>
                    </v-col>
                </v-row>
                <v-card-subtitle class="d-flex align-center text-center my-4">
                    <small class="mr-2 font-weight-bold">GENERALES</small>
                    <v-divider/>
                </v-card-subtitle>
                <v-row>
                    <v-col cols="12" md="6" sm="6" class="py-2">
                        <v-text-field v-model="data.dataCatCliente.nombre" prepend-inner-icon="mdi-account-question" density="compact" variant="outlined" 
                            hide-details label="Tipo de Cliente" placeholder="ingrese un tipo" 
                            persistent-placeholder :readonly="readonlyOption()"/>
                    </v-col>
                    <v-col cols="12" md="6" sm="6" class="py-2">
                        <v-text-field v-model="data.dataCatCliente.usuarioRegistro" prepend-inner-icon="mdi-account-cog" density="compact" variant="outlined" 
                            hide-details label="Usuario de Registro" placeholder="ingrese el usuario" 
                            persistent-placeholder :readonly="readonlyOption()"/>
                    </v-col>
                    <v-col cols="12" md="12" sm="12" class="py-2">
                        <v-textarea v-model="data.dataCatCliente.descripcion" prepend-inner-icon="mdi-text" density="compact" variant="outlined" 
                            hide-details label="descripcion" placeholder="..." 
                            persistent-placeholder :rows="2" :readonly="readonlyOption()"/>
                    </v-col>
                </v-row>
            </v-card-text>
            <v-divider/>
            <v-card-actions v-if="!localView">
                <v-btn color="grey" variant="outlined" @click="closeDialog()">
                    Cancelar
                </v-btn>
                <v-btn class="bg-red-darken-4" @click="handleSave()">
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
        prov: {
            type: Object,
            required: false
        },
        title: {
            type: String,
            required: true,
            default: 'Nuevo tipo de Cliente'
        },
        ver: {
            type: Boolean,
            required: false
        }
    },

    setup(props) {
        const localShow = ref(props.show)
        const localEdit = ref(props.editar)
        const localCat = ref(props.prov)
        const localTitle = ref(props.title)
        const localView = ref(props.ver)
        watch(() => props.show, (newValue) => {
            localShow.value = newValue
        })
        watch(() => props.editar, (val) => {
            localEdit.value = val
            if (val === true) {
                console.log();
                data.dataCatCliente.nombre = localCat.value.nombre
                data.dataCatCliente.descripcion = localCat.value.descripcion
                data.dataCatCliente.usuarioRegistro = localCat.value.usuarioRegistro
                data.idCat = localCat.value.idCategoriaCliente
            }
        })
        watch(() => props.prov, (val) => {
            localCat.value = val
        })
        watch(() => props.title, (val) => {
            localTitle.value = val
        })
        watch(() => props.ver, (val) => {
            localView.value = val
            if (val === true) {
                data.dataCatCliente.nombre = localCat.value.nombre
                data.dataCatCliente.descripcion = localCat.value.descripcion
                data.dataCatCliente.usuarioRegistro = localCat.value.usuarioRegistro
                data.idCat = localCat.value.idCategoriaCliente
            }
        })

        const data = reactive({
            nowDate: new Date(),
            dataCatCliente: {
                nombre: null,
                descripcion: null,
                usuarioRegistro: null
            },
            idCat: null,
            requestHttp: new RequestHttp()
        })

        return {
            localShow,
            localEdit,
            localTitle,
            localCat,
            localView,
            data
        }
    },

    methods: {
        async handleSave() {
            if (!this.data.dataCatCliente.nombre) {
                alert('Complete la informacion')
                return
            }
            if (!this.localEdit) {
                const result = await this.data.requestHttp.postCategoriaCliente(this.data.dataCatCliente)
    
                if (result !== null) {
                    alert('Registro Guardado')
                    this.$emit('closeDialog', false)
                    this.localEdit = false
                } else {
                    alert('No se pudo guardar el registro')
                }
            } else {
                if (!this.data.idCat) {
                    alert('Elija una categoria')
                    return
                }
                const result = await this.data.requestHttp.putCategoriaCliente(this.data.dataCatCliente, this.data.idCat)
                if (result !== null) {
                    alert('Registro Editado')
                    this.$emit('closeDialog', false)
                    this.localEdit = false
                } else {
                    alert('No se pudo editar el registro')
                }
            }
        },

        formatedDate(dataString) {
            const value = formatters.formatDate(dataString)
            return value
        },

        closeDialog() {
            this.$emit('closeDialog', false)
            this.data.dataCatCliente = {}
        },

        readonlyOption() {
            if (this.localView) {
                return true
            } else if(this.editar) {
                return false
            } else if(!this.editar && !this.localView) {
                return false
            }
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