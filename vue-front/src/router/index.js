import Vue from 'vue'
import Router from 'vue-router'
import market1 from '@/components/market1.vue'
import market2 from '@/components/market2.vue'
import main from '@/components/MainPage.vue'
import login from '@/components/Login.vue'
import registr from '@/components/Registration.vue'

Vue.use(Router)

export default new Router({
routes: [
    { 
    path: '/', name : 'main', component: main,
      children: [
          {
            // при совпадении пути с шаблоном /user/:id/profile
            // в <router-view> компонента User будет показан UserProfile
            path: 'market1',
            component: market1
          },
          {
            // при совпадении пути с шаблоном /user/:id/posts
            // в <router-view> компонента User будет показан UserPosts
            path: 'market2',
            component: market2
          }
        ]
    },
    { 
      path: '/login', component: login, 
    },
    { 
      path: '/registration', component: registr,
    }
]
})
