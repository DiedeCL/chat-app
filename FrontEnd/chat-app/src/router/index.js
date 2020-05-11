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
  }, {
    path: '/conversation',
    name: 'Conversation',
    component: () => import ('../views/Conversation')
  },
  {
    path: '/Files',
    name: 'Files',
    component: () => import ('../views/FilesView')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

router.beforeEach((to, from, next) => {
  const publicPages = ['/login', '/', '/signup'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('token');
  if (authRequired && !loggedIn ) return next('/login');
  next();
});

export default router;
