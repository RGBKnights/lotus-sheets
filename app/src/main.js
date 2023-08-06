
import '@formkit/themes/genesis'
import '@formkit/pro/genesis'

import { plugin as formKit, defaultConfig } from '@formkit/vue'
import { createProPlugin, rating, toggle } from '@formkit/pro'
import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

const app = createApp(App)
app.use(formKit, defaultConfig({
  plugins: [
    createProPlugin(import.meta.env.VITE_FORM_KIT_KEY, { rating, toggle })
  ],
}))
app.use(createPinia())
app.use(router)
app.mount('#app')