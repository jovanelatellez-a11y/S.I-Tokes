import { useStore } from '@/store'
import {
    createWebHistory,
    createRouter
} from 'vue-router'

const routes = createRouter({
    history: createWebHistory(),
    routes: [{
        path: '/',
        name: 'Home',
        component: () => import('@/components/pages/HomeApp.vue'),
        meta: {
            title: 'Inicio | T O K E S'
        }
    }, {
        path: '/gestion-productos',
        name: 'Productos',
        component: () => import('@/components/inventario/Productos/GestionProducto.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Productos | T O K E S'
        }
    }, {
        path: '/facturacion',
        name: 'Facturacion',
        component: () => import('@/components/movimientos/Ventas/FacturacionVentas.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Facturación | T O K E S'
        }
    }, {
        path: '/ordenes',
        name: 'Ordenes',
        component: () => import('@/components/movimientos/Compras/FacturacionCompras.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Compras | T O K E S'
        }
    }, {
        path: '/proveedores',
        name: 'Proveedores',
        component: () => import('@/components/movimientos/Compras/ProveedoresComp.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Proveedores | T O K E S'
        }
    }, {
        path: '/categorias',
        name: 'Categorias',
        component: () => import('@/components/inventario/Categorias/CategoriasProductos.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Categorías | T O K E S'
        }
    }, {
        path: '/clientes',
        name: 'Clientes',
        component: () => import('@/components/clientes/ClientesComp.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Clientes | T O K E S'
        }
    }, {
        path: '/cpc',
        name: 'CPC',
        component: () => import('@/components/clientes/CPComp.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'CxC | T O K E S'
        }
    }, {
        path: '/movimientos',
        name: 'Movimientos',
        component: () => import('@/components/inventario/Movimientos/MovimientosComp.vue'),
        meta: {
            keepAlive: true,
            requiredAuth: true,
            title: 'Movimientos | T O K E S'
        }
    }],

    scrollBehavior(to, from, savePosition) {
        if (savePosition) {
            return savePosition
        } else {
            return {
                top: 0
            }
        }
    }
})

routes.beforeEach((to, from, next) => {
    const store = useStore()
    const isLoggedIn = store.isLoggedIn
    const defaultTitle = 'T O K E S'
    if (!isLoggedIn) {
        document.title = defaultTitle
    } else {
        document.title = to.meta.title
    }

    !isLoggedIn && to.meta.requiredAuth ? next({ path: '/' }) : next()
})

export default routes