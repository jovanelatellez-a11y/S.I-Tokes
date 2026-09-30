import { defineStore } from "pinia";
import { ref } from "vue";

export const useStore = defineStore('auth', {
    state: () => ({
        isLoggedIn: !!localStorage.getItem("token"),
        // isLoggedIn: true,
        theme: ref(localStorage.getItem('theme') || 'dark'),
    }),
    actions: {
        login() {
            localStorage.setItem("token", "logged-in");
            this.isLoggedIn = true;
        },
        logout() {
            localStorage.removeItem("token");
            localStorage.removeItem("rol");
            localStorage.removeItem("name");
            localStorage.removeItem('exp');
            sessionStorage.removeItem('lastRoute')
            this.isLoggedIn = false;
        },
        sendRolUser(rol) {
            localStorage.setItem("rol", rol)
        },
        getRolUser() {
            return localStorage.getItem("rol")
        },
        sendNameUser(name) {
            localStorage.setItem('name', name)
        },
        getNameUser() {
            return localStorage.getItem('name')
        },
        sendExp(exp) {
            localStorage.setItem('exp', exp)
        },
        deleteSession(exp) {
            const now = Date.now()
            if (now >= exp) {
                this.logout()
                localStorage.removeItem('exp')
                return console.log('Sesión expirada');
            } else {
                return console.log('Sesión activa');
            }   
        },
        setTheme(theme) {
            localStorage.setItem('theme', theme)
            this.theme = theme
        },
    },
    getters: {
        getIsLoggedIn: (state) => state.isLoggedIn
    },
});