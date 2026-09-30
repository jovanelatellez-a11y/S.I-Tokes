<template>
    <v-dialog v-model="localShow" max-width="400">
      <v-card rounded="xl">
        <v-card-title class="text-h6 font-weight-bold d-flex justify-center align-center">
          <v-icon color="red-darken-4">mdi-delete</v-icon>
          <span>Confirmar Eliminación</span>
        </v-card-title>
        <v-divider class="mx-4"/>
        <v-card-text class="text-center">
          ¿Estás seguro de que deseas eliminar elemento"?
        </v-card-text>
        <v-divider class="mx-4"/>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="grey" @click="cancelAction()">Cancelar</v-btn>
          <v-btn color="error" @click="deleteItem()">Eliminar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script>
import { ref, watch } from 'vue';

export default {
    props: {
        show: {
            type: Boolean,
            required: true
        }
    },

    setup(props) {
        const localShow = ref(props.show)
        watch(() => props.show, (val) => {
            localShow.value = val
        })

        return {
            localShow
        }
    },

    methods: {
        deleteItem() {
            this.$emit('deleteItem', true)
            this.localShow = false
        },  

        cancelAction() {
            this.$emit('deleteItem', false)
            this.localShow = false
        }
    }
}
</script>