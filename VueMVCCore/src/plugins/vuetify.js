import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import 'vuetify/dist/vuetify.min.css'
import '@mdi/font/css/materialdesignicons.css'

Vue.use(Vuetify);

const opts = {
    theme: {
        dark: true,
        themes: {
            dark: {
                custom_red: '#C44058',
                custom_green: '#76E29C',
                primary: '#E00074',
                custom_grey: '#828089',
                custom_purple: '#877DC6'
            }
        }
    }
}

export default new Vuetify(opts)