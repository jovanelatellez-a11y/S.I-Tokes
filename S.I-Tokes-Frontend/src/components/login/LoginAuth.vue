<template>
    <div id="cont">
        <LoaderComp :data="data.msgLoader" v-show="data.loading" />
        <div id="logo-auth" class="d-flex flex-column justify-center align-center">
            <div>
                <img width="200px" class="ml-10" src="/logo.png" alt="TOKES"/>
            </div>
            <v-card id="card-form" elevation="0" class="bg-transparent border" rounded="lg">
                <v-card-title class="text-center mt-2"><strong>INICIO DE SESION</strong></v-card-title>
                <v-form @submit.prevent="authLoging()" id="form-login" v-model="data.valid" class="mx-2 mb-8 px-4" ref="form" lazy-validation>
                    <v-card-text class="pt-0">
                        <div class="text-subtitle-1 text-medium-emphasis">Usuario</div>
                        <v-text-field color="red-darken-4" v-model="data.data.usuario" placeholder="Ingrese su usuario" type="email" 
                            :rules="data.rules.userRules" @input="clearError" variant="outlined" prepend-inner-icon="mdi-account"
                            aria-describedby="input-username-messages" autocomplete="username" density="compact" />
                            <div class="text-subtitle-1 text-medium-emphasis">Contraseña</div>
                        <v-text-field color="red-darken-4" v-model="data.data.password" placeholder="Ingrese su contraseña" autocomplete="current-password" @click:append-inner="passVisible"
                            :append-inner-icon="data.showPass ? 'mdi-eye' : 'mdi-eye-off'" prepend-inner-icon="mdi-lock" @input="clearError"
                            variant="outlined" density="compact" :type="data.showPass ? 'text': 'password'" :rules="data.rules.passRules"/>
                    </v-card-text>
                    <v-fade-transition>
                        <v-alert density="compact" v-if="data.errorMsg" type="warning" variant="tonal">{{
                            data.errorMsg
                            }}</v-alert>
                    </v-fade-transition>
                    <v-card-actions id="btn" class="pb-8 d-flex justify-center">
                        <v-btn  type="submit" size="large" @click="authLoging()" class="bg-red-darken-4" block>Acceder</v-btn>
                    </v-card-actions>
                </v-form>
            </v-card>
        </div>
    </div>
</template>

<script>
import LoaderComp from '../widgets/LoaderComp.vue';
import { useStore } from '@/store';
import { reactive } from 'vue';

export default {
    name: 'LoginAuth',

    setup() {
        const useAuth = useStore()
        const data = reactive({
            rules: {
                userRules: [v => !!v || 'El campo es obligatorio'], 
                passRules: [v => !!v || 'El campo es obligatorio']
            },
            data: {
                usuario: '',
                password: ''
            },
            loading: false,
            valid: false,
            errorMsg: '',
            showPass: false,
            isVisible: false,
            msgLoader: '',
        })

        return {
            data,
            useAuth
        }
    },

    components: {
        LoaderComp
    },

    methods: {
        passVisible() {
            this.data.showPass = !this.data.showPass;
        },

        clearError() {
            this.data.errorMsg = '';
        },

        delay(ms) {
            return new Promise(resolve => setTimeout(resolve, ms))
        },

        async authLoging() {
            this.$refs.form.validate();

            if(!this.data.data.usuario || !this.data.data.password){
                return;
            }
            
            if (this.data.data.usuario == 'Admin' && this.data.data.password == 'admin123') {
                this.data.loading = true
                this.data.msgLoader = 'Iniciando Sesion'
                await this.delay(1500);
                const token = 'affwrfq425buefkwb2rob'
                this.useAuth.login(token)
                this.data.loading = false
            } else {
                this.data.errorMsg = 'Credenciales incorrectas';
                this.data.loading = false;
            }
        }
    }
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap');

#cont{
    width: 100%;
    height: 100vh;
    font-family: Poppins;
    /* position: relative; */
}

.v-app-bar{
    background-color: transparent !important;
}

div.v-toolbar__content {
  height: 80px !important; /* Ajusta el valor según lo que necesites */
}

#cont-form{
    width: 100%;
    height: 100%;
}

#card-form{
    width: 380px;
}

#title{
    position: absolute;
    top: 20%;
    left: 50%;
    transform: translate(-50%, -50%);
}

#logo-form{
    width: 400px;
    position: absolute;
    top: 25%;
    left: 50%;
    transform: translate(-50%, -50%);
}


#title{
    font-weight: 500;
}

#text-logo{
    color: #08529D;
}

#btn-ac{
    background-color: #08529D;
}

@keyframes visibleLogo {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}

.bg-logo{
    animation: visibleLogo 1s ease-in-out forwards;
    width: 600px;
    position: absolute;
    top: 59%;
    left: 10%;
    transform: translate(-50%, -50%) rotate(360deg);
    transform-origin: center;
}

@media screen and (max-width: 600px) {
    /* .bg-logo{
       width: 600px; 
       left: -60%;
    } */
}
</style>