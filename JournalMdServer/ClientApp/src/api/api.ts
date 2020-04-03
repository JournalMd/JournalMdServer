import Vue from 'vue';
import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:44302/api/'; // TODO change to real api - process.env.JOURNALMD_API_LOCATION;
axios.defaults.headers.common.Accept = 'application/json';
axios.interceptors.response.use(
  response => response,
  (error) => {
    if (error.response.status === 401) {
      console.debug('TODO: 401 force logout');
      // TODO: store.dispatch('auth/logout'); // how to use without store/router import (cycle!)
    }

    return Promise.reject(error);
  },
);

export const axiosAuthenticated = axios.create({
});
axiosAuthenticated.defaults.headers.common.Authorization = `Bearer ${localStorage.getItem('authToken')}`;

export const axiosUnauthenticated = axios.create({
});
