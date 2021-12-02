import Vue from 'vue'
import VueRouter from 'vue-router'
import Employees from '@/components/Employees.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Employees',
    component: Employees
  },
  {
    path: '/about',
    name: 'About',
    component: () => import(/* webpackChunkName: "about" */ '@/components/About.vue')
  },
  {
    path: '/past-employees',
    name: 'Past Employees',
    component: () => import(/* webpackChunkName: "about" */ '@/components/PastEmployees.vue')
  },
  {
    path: '/positions',
    name: 'Positions',
    component: () => import(/* webpackChunkName: "about" */ '@/components/Positions.vue')
  },
  {
    path: '*',
    redirect: '/'
  }
]

const router = new VueRouter({
  routes
})

export default router
