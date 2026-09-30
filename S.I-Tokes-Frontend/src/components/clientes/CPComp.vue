<template>
    <div>
        <v-card class="border" elevation="0" rounded="0">
            <v-card-title class="d-flex align-center">
                <h5><v-icon color="red-darken-4">mdi-hand-coin</v-icon>Cuentas por Cobrar</h5>
                <v-spacer />
                <v-btn icon size="small" color="white" variant="tonal" @click="closeDialog()">
                    <v-icon>mdi-close</v-icon>
                    <v-tooltip activator="parent" location="top" text="Cerrar" />
                </v-btn>
            </v-card-title>
        </v-card>
    </div>
</template>

<script>
export default {
    
}
</script>

<style scoped>

</style>