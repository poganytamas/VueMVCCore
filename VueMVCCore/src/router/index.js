import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'home',
        component: require('@views/Home').default
    },
    {
        path: '/unauthorized',
        name: 'unauthorized',
        component: require('@views/Unauthorized').default
    },
    {
        path: '/about',
        name: 'about',
        component: require('@views/About').default
    },
    {
        path: '*',
        name: 'notfound',
        component: require('@views/NotFound').default
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
