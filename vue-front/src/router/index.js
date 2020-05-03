import Vue from 'vue'
import Router from 'vue-router'
import market1 from '@/components/market1.vue'
import market2 from '@/components/market2.vue'
import market3 from '@/components/market3.vue'
import main from '@/components/MainPage.vue'
import login from '@/components/Login.vue'
import registr from '@/components/Registration.vue'
import personal from '@/components/Personal.vue'
Vue.use(Router)

export default new Router({
routes: [
    { 
    path: '/', name : 'main', component: main,
      children: [
          {
            path: 'market1',
            name : 'market1',
            component: market1,
            props: true
          },
          {
            path: 'market2',
            name : 'market2',
            component: market2,
            props: true
          },
          {
            path: 'market3',
            name : 'market3',
            component: market3,
            props: true
          }
        ]
    },
    { 
      path: '/login', component: login, 
    },
    { 
      path: '/registration', component: registr,
    },
    { 
      path: '/personal', component: personal,
    }
]
})
