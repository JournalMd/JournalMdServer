import Vue from 'vue';
import VueRouter, { Route } from 'vue-router';
import store from '../store';
import Dashboard from '../views/Dashboard.vue';

Vue.use(VueRouter);

function requireAuth(to: Route, from: Route, next: any) {
  if (!(store.state as any).auth.authenticated) {
    next({
      path: '/login',
      // query: { redirect: to.fullPath, from: from.fullPath },
    });
  } else {
    next();
  }
}

// Use webpackChunkName for route level code-splitting => lazy-loaded
const routes = [
  {
    path: '/',
    name: 'dashboard',
    component: Dashboard,
    beforeEnter: requireAuth,
  },
  {
    path: '/login',
    name: 'login',
    component: () => import(/* webpackChunkName: "login" */ '../views/Login.vue'),
  },
  {
    path: '/register',
    name: 'register',
    component: () => import(/* webpackChunkName: "register" */ '../views/Register.vue'),
  },
  {
    path: '/logout',
    beforeEnter(to: any, from: any, next: any) {
      store.dispatch('auth/logout');
      next('/login');
    },
  },
  {
    path: '/test/:testParam',
    name: 'test',
    component: () => import(/* webpackChunkName: "test" */ '../views/Test.vue'),
    beforeEnter: requireAuth,
  },
  {
    path: '/fastentry',
    name: 'fastentry',
    component: () => import(/* webpackChunkName: "fastentry" */ '../views/FastEntry.vue'),
    beforeEnter: requireAuth,
  },
  {
    path: '/entry',
    name: 'entry',
    component: () => import(/* webpackChunkName: "entry" */ '../views/Entry.vue'),
    beforeEnter: requireAuth,
  },
  {
    path: '/types/:type',
    name: 'types',
    props: true,
    component: () => import(/* webpackChunkName: "overview" */ '../views/Overview.vue'),
    beforeEnter: requireAuth,
  },
  {
    path: '/profile',
    name: 'userprofile',
    component: () => import(/* webpackChunkName: "userprofile" */ '../views/UserProfile.vue'),
    beforeEnter: requireAuth,
  },
  {
    path: '/settings',
    name: 'usersettings',
    component: () => import(/* webpackChunkName: "usersettings" */ '../views/UserSettings.vue'),
    beforeEnter: requireAuth,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
