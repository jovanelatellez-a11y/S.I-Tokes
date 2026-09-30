<template>
  <div class="w-100">
    <!-- Encabezado y botón de agregar -->
    <v-card class="border" elevation="0" rounded="0">
    <!-- Encabezado -->
      <template v-slot:prepend>
        <div class="d-flex align-center">
          <!-- Título -->
          <div class="text-h6 font-weight-bold d-flex align-center">
            <v-icon class="me-2" color="red-darken-4">mdi-package-variant</v-icon>
            Productos
          </div>
        </div>
      </template>
      <template v-slot:append>
        <v-btn class="bg-red-darken-4 rounded-" @click="openDialog('create')">
          <v-icon>mdi-plus</v-icon>
          <v-tooltip activator="parent" location="left">Agregar Producto</v-tooltip> 
        </v-btn>
      </template> 
      <v-divider />

      <v-row class="pa-2" dense>
        <v-col cols="6" md="6" sm="6">
            <v-text-field color="red-darken-4" density="compact" variant="outlined" append-inner-icon="mdi-magnify" label="Buscar productos"
              hide-details placeholder="Ingrese un texto a buscar..." persistent-placeholder/>
        </v-col>
        <v-col cols="6" md="6" sm="6" class="d-flex justify-end align-center">
            <v-btn icon color="red-darken-4" size="small" variant="text" class="mr-2 border">
              <v-icon>mdi-magnify</v-icon>
            </v-btn>
            <v-btn icon color="grey" size="small" variant="text" class="border">
              <v-icon>mdi-broom</v-icon>
            </v-btn>
        </v-col>
      </v-row>
    </v-card>
    <!-- Tabla de productos -->
    <v-card elevation="0" class="border" rounded="0">
      <v-data-table class="font" density="compact" :headers="data.headers" :items="data.products" :items-per-page="10" :search="search" :loading="data.loading">
        <template v-slot:item.precio="{ item }">
          {{ formatCurrency(item.precio) }}
        </template>
        
        <template v-slot:item.fechaRegistro="{ item }">
          {{ formatDate(item.fechaRegistro) }}
        </template>
        
        <template v-slot:item.estado="{ item }">
          <v-chip :color="item.estado ? 'green' : 'error' " small>
            {{ item.estado ? 'Activo' : 'Inactivo' }}
          </v-chip>
        </template>
        
        <template v-slot:item.actions="{ item }">
          <v-tooltip text="Editar" location="top">
            <template v-slot:activator="{ props }">
              <v-icon v-bind="props" size="small" color="green" class="mr-1" @click="openDialog('edit', item)" >mdi-pencil</v-icon>
            </template>
          </v-tooltip>
          
          <v-tooltip text="Eliminar" location="top">
            <template v-slot:activator="{ props }">
              <v-icon v-bind="props" size="small" color="error" class="mr-1" @click="showAlert(item)">mdi-delete</v-icon>
            </template>
          </v-tooltip>

          <v-tooltip text="Ver" location="top">
            <template v-slot:activator="{ props }">
              <v-icon v-bind="props" size="small" color="indigo-darken-4" @click="openDialogDet(item)">mdi-eye</v-icon>
            </template>
          </v-tooltip>
        </template>
      </v-data-table>
    </v-card>

    <!-- Diálogo para agregar/editar -->
    <v-dialog v-model="dialog" max-width="600" persistent>
      <v-card class="w-100 mb-6" elevation="0">
        <v-card-title class="text-h5 text-center pa-1 font-weight-bold bg-red-darken-4">
          <v-icon>mdi-package-variant</v-icon>
          Inventario - Registro de Productos
        </v-card-title>
        <v-divider></v-divider>
        <v-card-text class="pt-4">
          <v-form class="w-100" ref="form">
            <v-row dense>
              <v-col cols="12" md="6" sm="6">
                <v-text-field color="red-darken-4" v-model="data.form.codigo" label="Código" 
                  :rules="[rules.required]" variant="outlined" 
                  hide-details density="compact" clearable prepend-inner-icon="mdi-barcode"/>
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-text-field color="red-darken-4" v-model="data.form.nombre" label="Nombre" 
                  :rules="[rules.required, rules.minLength(3)]"  variant="outlined" 
                  hide-details density="compact" clearable prepend-inner-icon="mdi-text-box" />
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-text-field color="red-darken-4" v-model="data.form.precio" label="Precio" 
                  :rules="[rules.required, rules.numeric]" variant="outlined" 
                  hide-details density="compact" type="number" step="0.01" prepend-inner-icon="mdi-currency-usd" />
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-text-field color="red-darken-4" v-model="data.form.costo" label="Costo" 
                  :rules="[rules.required, rules.numeric]" variant="outlined" 
                  hide-details density="compact" type="number" step="0.01" prepend-inner-icon="mdi-cash" />
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-autocomplete color="red-darken-4" label="Und. Medidad" v-model="data.form.idUnidadMedida"
                  :rules="[rules.required, rules.numeric]" variant="outlined" 
                  hide-details density="compact" step="0.01" />
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-select v-model="data.form.idSubCatProd" label="Sub categoría" :items="data.subCategorias" 
                  :rules="[rules.required]" variant="outlined" hide-details density="compact" prepend-inner-icon="mdi-shape-outline" />
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-text-field v-model="data.form.tipoProducto" label="Tipo Producto" :rules="[rules.required]" variant="outlined" 
                  hide-details density="compact" prepend-inner-icon="mdi-tag" />
              </v-col>
              <v-col cols="6" md="6" sm="6">
                <v-text-field color="red-darken-4" v-model="data.form.cantidadTotal" label="Stock" 
                  :rules="[rules.required, rules.numeric]" variant="outlined" 
                  hide-details density="compact" type="number" prepend-inner-icon="mdi-numeric" />
              </v-col>
              <v-col cols="6" md="6" sm="6">
                <v-text-field color="red-darken-4" v-model="data.form.cantidadMinima" label="Stock Mínimo" 
                  :rules="[rules.required, rules.numeric]" variant="outlined" 
                  hide-details density="compact" type="number" prepend-inner-icon="mdi-numeric" />
              </v-col>
              <v-col cols="12" md="6" sm="6">
                <v-file-input density="compact" variant="outlined" label="Selecciona una imagen" accept="image/*" 
                  @update:model-value="convertirImagen" prepend-inner-icon="mdi-image" />
              </v-col>
              <v-col cols="12" md="12" sm="12">
                <v-textarea v-model="data.observaciones" density="compact" variant="outlined" label="Observaciones" prepend-inner-icon="mdi-text" />
              </v-col>
            </v-row>
          </v-form>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="grey" variant="outlined" @click="closeDialog()">
            Cerrar
          </v-btn>
          <v-btn class="bg-red-darken-4" @click="handleSave(data.form)">
            Guardar
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <DetallesProducto :show="data.showDialog" :producto="data.productDialog" @cerrarDialog="closeDialogDet"/>
    <AlertComp :show="data.viewAlert" @deleteItem="deleteAction"/>
  </div>
</template>

<script>
import { ref, reactive, computed, onMounted, onUnmounted } from 'vue'
import { utilsFunctions } from '@/helpers/utilFunctions'
import DetallesProducto from './modalsProductos/DetallesProducto.vue'
import RequestHttp from '@/services/requestHttp'
import AlertComp from '@/components/widgets/AlertComp.vue'

export default {
  mounted() {
    this.getSubCategorias()
    this.getProductos()
  },

  components: {
    DetallesProducto,
    AlertComp
  },
  
  setup() {
    const screenWidth = ref(window.innerWidth)
    const isMobile = computed(() => screenWidth.key < 600)
    const updateScreen = () => {
      screenWidth.key = window.innerWidth
    }
    onMounted(() => {
      window.addEventListener('resize', updateScreen)
    })
    onUnmounted(() => {
      window.addEventListener('resize', updateScreen)
    })

    const data = reactive({
      products: [],
      headers: [
        { title: 'Acciones', key: 'actions', sortable: false, align: 'center' },
        { title: 'Código', key: 'codigo', align: 'center' },
        { title: 'Nombre', key: 'nombre', align: 'center' },
        { title: 'Precio', key: 'precio', align: 'center' },
        { title: 'Costo', key: 'costo', align: 'center' },
        { title: 'Stock', key: 'cantidadTotal', align: 'center' },
        { title: 'Stock Min', key: 'cantidadMinima', align: 'center' },
        { title: 'Sub categoría', key: 'categoria', align: 'center' },
        { title: 'Fecha Registro', key: 'fechaRegistro', align: 'center' },
        { title: 'Estado', key: 'estado', align: 'center' },
      ],
      subCategorias: [],
      form: {
        codigo: null,
        nombre: null,
        precio: 0,
        costo: 0,
        idSubCatProd: null,
        idUnidadMedida: 1,
        tipoProducto: null,
        cantidadTotal: 0,
        cantidadMinima: 0,
        imagen: null,
        usuarioRegistro: 'admin'
      },
      observaciones: null,
      selectedProduct: null,
      loading: false,
      showDialog: false,
      viewAlert: false,
      requestHttp: new RequestHttp()
    })
    
    return { 
      data,
      isMobile,
    }
  },
  
  data() {
    return {
      search: '',
      dialog: false,
      deleteDialog: false,
      dialogMode: 'create',
      selectedProduct: null,
      productToDelete: null,
      rules: {
        required: key => !!key || 'Campo requerido',
        minLength: min => key => (key && key.length >= min) || `Mínimo ${min} caracteres`,
        numeric: key => !isNaN(parseFloat(key)) || 'Debe ser un número válido'
      }
    }
  },
  
  computed: {
    dialogTitle() {
      return this.dialogMode === 'create' ? 'Nuevo Producto' : 'Editar Producto'
    },
  },
  
  methods: {
    async getSubCategorias() {
      this.data.subCategorias = []
      this.data.loading = true
      const result = await this.data.requestHttp.getSubCategorias()
      this.data.loading = false
      if (result !== null) {
        result.map(item => {
          this.data.subCategorias.push({title: item.nombre, value: item.idSubCatProd})
        })
        
      } else {
        throw new Error('Error en la solicitud')
      }
    },    

    async getProductos() {
      this.data.products = []
      this.data.loading = true
      const result = await this.data.requestHttp.getProductos()
      this.data.loading = false

      if (result !== null) {
        const promises = result.map(async (item) => {
          const categoria = await this.data.requestHttp.getByIdSubCategorias(item.idSubCatProd)
          return {...item, categoria: categoria.nombre}
        })
        this.data.products = await Promise.all(promises)
      } else {
        throw new Error('Error en la solicitud')
      }
    },

    openDialog(mode, product = null) {
      this.dialogMode = mode
      if (product) {
        this.selectedProduct = product.idProducto
        this.data.form.codigo = product.codigo
        this.data.form.costo = product.costo
        this.data.form.categoria = product.categoria
        this.data.form.nombre = product.nombre
        this.data.form.precio = product.precio
        this.data.form.categoria = product.categoria
        this.data.form.idSubCatProd = product.idSubCatProd
        this.data.form.tipoProducto = product.tipoProducto
        this.data.form.cantidadTotal = product.cantidadTotal
        this.data.form.cantidadMinima = product.cantidadMinima
        this.data.form.usuarioRegistro = product.usuarioRegistro
        this.data.form.imagen = product.imagen
      }
      this.dialog = true
    },

    openDialogDet(obj) {
      this.data.showDialog = !this.data.showDialog,
      this.data.productDialog = obj
    },

    closeDialogDet(key) {
      this.data.showDialog = key
    },
    
    closeDialog() {
      this.dialog = false
      this.selectedProduct = null
      this.data.form = {}
    },
    
    async handleSave(productData) {
      this.data.form.costo = Number(this.data.form.costo)
      this.data.form.precio = Number(this.data.form.precio)
      this.data.form.cantidadMinima = Number(this.data.form.cantidadMinima)
      this.data.form.cantidadTotal = Number(this.data.form.cantidadTotal)
      const valid = utilsFunctions.objectValidate(productData)
      this.data.form.imagen = null

      if (this.dialogMode === 'create') {
        if (!valid) {
          alert('Complete toda la informacion')
          return
        }

        try {
          const result = await this.data.requestHttp.postProducto(productData)
          if (result !== null) {
            alert('Registro Guardado')
            this.getProductos()
          } else {
            alert('Error al guardar el producto')
          }
        } catch (error) {
          throw new Error('Error en la solicitud', error)
        }

      } else {
        if (!valid) {
          alert('Complete toda la informacion')
          return
        }
        try {
          const result = await this.data.requestHttp.putProductos(productData, this.selectedProduct)
          if (result !== null) {
            alert('Registro Editado')
            this.getProductos()
          } else {
            alert('Error al guardar el producto')
          }
        } catch (error) {
          throw new Error('Error en la solicitud', error)
        }
      }
      this.closeDialog()
    },
    
    deleteAction(val) {
      if (val === true) {
        this.deleteItem()
      }
      this.data.viewAlert = false
    },

    showAlert(item){
      this.data.viewAlert = true
      this.data.selectedProduct = item.idProducto
    },

    async deleteItem() {
        const result = await this.data.requestHttp.deleteProducto(this.data.selectedProduct)
        if (result !== null) {
          alert('Producto Eliminado')
          this.getProductos()
        } else {
          alert('No se pudo eliminar el registro')
        }
    },
    
    formatCurrency(key) {
      return new Intl.NumberFormat('es-MX', {
        style: 'currency',
        currency: 'MXN'
      }).format(key)
    },
    
    formatDate(dateString) {
      if (!dateString) return ''
      const date = new Date(dateString)
      return date.toLocaleDateString('es-MX', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
      })
    },
    
    getStatusColor(status) {
      const statusColors = {
        'Activo': 'success',
        'Inactivo': 'warning',
        'Descontinuado': 'error'
      }
      return statusColors[status] || 'grey'
    },

    convertirImagen(archivo) {
      if (!archivo) return;
      // Si es múltiple, toma el primero
      const file = Array.isArray(archivo) ? archivo[0] : archivo;

      const lector = new FileReader();
      lector.onload = () => {
        this.data.form.imagen = lector.result;
      };

      lector.readAsDataURL(file);
    }
  }
}
</script>

<style scoped>

.v-card-title {
  border-radius: 4px 4px 0 0;
}

.font{
  font-size: 12px !important;
}
</style>