import { createApp } from 'vue'
import { createPinia } from 'pinia'
import routes from './routes/routes.js'
import App from './App.vue'

import { createVuetify } from 'vuetify/lib/framework.mjs'
import '@mdi/font/css/materialdesignicons.css'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import 'vuetify/styles'
import Axios from 'axios'

Axios.defaults.baseURL = 'http://localhost:5091/'

const pinia = createPinia();
const app = createApp(App);
app.use(pinia);

const vuetify = createVuetify({
    components,
    directives,
    icons: {
        defaultSet: 'mdi'
    },
    theme: {
        defaultTheme: 'light',
        themes: {
            dark: "dark",
            light: "light"
        }
    }
})

app.use(vuetify).use(routes);

app.mount('#app')