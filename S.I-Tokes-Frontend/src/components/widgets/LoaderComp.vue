<template>
    <div class="loader-overlay">
      <div class="loader-card">
        <v-card class="text-center pa-2" id="card-logo">
          <v-card-text>
            <img src="/logo.png" alt="loading" class="loading-image" />
            <h4 class="mt-2">{{ data }}</h4>
            <v-progress-linear color="indigo" indeterminate rounded height="5" class="mb-4"/>
          </v-card-text>
        </v-card>
      </div>
    </div>
</template>
  
<script>
import { reactive } from 'vue';

  export default {
    props: {
      data: {
        type: String,
      }
    },

    name: "LoaderCard",

    setup() {
      const state = reactive({
      })

      return {
        state
      }
    }
  };
  </script>
  
<style scoped>
@keyframes opacityOn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}
  .loader-overlay {
    animation: opacityOn 1s;
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background-color: rgba(0, 0, 0, 0.5); /* Fondo oscuro translúcido */
    z-index: 1000; /* Asegura que esté encima de otros elementos */
  }

  #card-logo{
    width: 250px;
    height: 150px;
  }
  
  .loader-card {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 1001;
  }
  
  .loading-image {
    width: 80px;
    height: 100%;
    /* animation: spin 2s linear infinite; */
  }
  
  @keyframes spin {
    from {
      transform: rotate(0deg);
    }
    to {
      transform: rotate(360deg);
    }
  }
</style>
  