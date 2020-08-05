import Vue from 'vue'
import App from '@app/App'
import vuetify from './plugins/vuetify';
import router from './router'

new Vue({
    vuetify,
    router,
    render: h => h(App)
}).$mount('#app')
