import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import ('../views/Home')
  },
   {
    path: '/login',
    name: 'Login',
    component: () => import ('../views/Login')
  },
 
  {
    path: '/signup',
    name: 'SignUP',
    component: () => import ('../views/Signup')
  },
  {
    path: '/conversations',
    name: 'Conversations',
    component: () => import ('../views/Conversations')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router;
