import Vue from 'vue';
import { axiosAuthenticated, axiosUnauthenticated } from '@/api/api';
import router from '@/router';
import {
  CHECK,
  REGISTER,
  LOGIN,
  LOGOUT,
} from './mutation-types';

/* eslint-disable no-param-reassign */
export default {
  [CHECK](state: any) {
    state.authenticated = !!localStorage.getItem('authToken');
    if (state.authenticated) {
      axiosAuthenticated.defaults.headers.common.Authorization = `Bearer ${localStorage.getItem('authToken')}`;
    }
  },

  [REGISTER]() {
    //
  },

  [LOGIN](state: any, token: string) {
    state.authenticated = true;
    localStorage.setItem('authToken', token);
    axiosAuthenticated.defaults.headers.common.Authorization = `Bearer ${token}`;

    router.push('/');
  },

  [LOGOUT](state: any) {
    state.authenticated = false;
    localStorage.removeItem('authToken');
    axiosAuthenticated.defaults.headers.common.Authorization = '';
  },
};
