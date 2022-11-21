import '@babel/polyfill'
import 'mutationobserver-shim'
import Vue from 'vue'
import './plugins/bootstrap-vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'
import 'vue-search-select/dist/VueSearchSelect.css'

Vue.config.productionTip = false

Vue.prototype.$http = axios.create({
  baseURL: 'https://localhost:44337/api'
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
