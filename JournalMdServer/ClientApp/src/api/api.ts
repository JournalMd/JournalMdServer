import axios from 'axios';

export const axiosAuthenticated = axios.create({
  baseURL: 'https://jsonplaceholder.typicode.com/', // TODO change to real api
});
axiosAuthenticated.defaults.headers.common.Authorization = `Token ${localStorage.getItem('authToken')}`;

export const axiosUnauthenticated = axios.create({
  baseURL: 'https://jsonplaceholder.typicode.com/', // TODO change to real api
});
