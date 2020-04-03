import Vue from 'vue';
import VueRouter from 'vue-router';
import Dashboard from '../views/Dashboard.vue';

Vue.use(VueRouter);

// Use webpackChunkName for route level code-splitting => lazy-loaded
const routes = [
  {
    path: '/',
    name: 'dashboard',
    component: Dashboard,
  },
  {
    path: '/test/:testParam',
    name: 'test',
    component: () => import(/* webpackChunkName: "test" */ '../views/Test.vue'),
  },
  {
    path: '/fastentry',
    name: 'fastentry',
    component: () => import(/* webpackChunkName: "fastentry" */ '../views/FastEntry.vue'),
  },
  {
    path: '/entry',
    name: 'entry',
    component: () => import(/* webpackChunkName: "entry" */ '../views/Entry.vue'),
  },
  {
    path: '/types/:type',
    name: 'types',
    props: true,
    component: () => import(/* webpackChunkName: "overview" */ '../views/Overview.vue'),
  },
  {
    path: '/profile',
    name: 'userprofile',
    component: () => import(/* webpackChunkName: "userprofile" */ '../views/UserProfile.vue'),
  },
  {
    path: '/settings',
    name: 'usersettings',
    component: () => import(/* webpackChunkName: "usersettings" */ '../views/UserSettings.vue'),
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
