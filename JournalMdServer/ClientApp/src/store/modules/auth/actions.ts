import Vue from 'vue';
import { Commit, Dispatch } from 'vuex';
import { axiosAuthenticated, axiosUnauthenticated } from '../../../api/api';
import * as types from './mutation-types';

export const check = ({ commit }: { commit: Commit }) => {
  commit(types.CHECK);
};

export const register = ({ commit }: { commit: Commit }) => {
  // TODO IMPLEMENT

  commit(types.LOGIN, 'RandomGeneratedToken');
};

export const login = ({ commit, dispatch }: { commit: Commit, dispatch: Dispatch },
  credentials: { username: string, password: string }) => {
  axiosUnauthenticated.post('Users/authenticate', credentials)
    .then((result) => {
      console.log('login result', result);
      commit(types.LOGIN, result.data.token);
    })
    .catch((error) => {
      console.log('Request failed...', error);
      dispatch('dialogs/addError', error.response.data.message, { root: true });
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
