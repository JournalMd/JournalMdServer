import Vue from 'vue';
import { Commit } from 'vuex';
import { axiosAuthenticated, axiosUnauthenticated } from '@/api/api';
import * as types from './mutation-types';

export const check = ({ commit }: { commit: Commit }) => {
  commit(types.CHECK);
};

export const register = ({ commit }: { commit: Commit }) => {
  // TODO IMPLEMENT

  commit(types.LOGIN, 'RandomGeneratedToken');
};

export const login = ({ commit }: { commit: Commit }, credentials: { username: string, password: string }) => {
  axiosUnauthenticated.post('Users/authenticate', credentials)
    .then((result) => {
      console.log('login result', result);
      commit(types.LOGIN, result.data.token);
    })
    .catch((error) => {
      console.log('Request failed...', error);
    });
};

export const logout = ({ commit }: { commit: Commit }) => {
  commit(types.LOGOUT);
};

export default {
  check,
  register,
  login,
  logout,
};
