import Vue from 'vue';
import { Commit, Dispatch } from 'vuex';
import { axiosAuthenticated, axiosUnauthenticated } from '../../../api/api';
import * as types from './mutation-types';

export const check = ({ commit }: { commit: Commit }) => {
  commit(types.CHECK);
};

export const register = ({ commit, dispatch }: { commit: Commit, dispatch: Dispatch },
  credentials: { username: string, password: string }) => {
  axiosUnauthenticated.post('users/register', credentials)
    .then((result) => {
      dispatch('dialogs/addMessage', result.data.username, { root: true });
      commit(types.REGISTER, result.data.username);
    })
    .catch((error) => {
      dispatch('dialogs/addError', error.response.data.message, { root: true });
      commit(types.REGISTER_FAILED);
    });
};

export const login = ({ commit, dispatch }: { commit: Commit, dispatch: Dispatch },
  credentials: { username: string, password: string }) => {
  axiosUnauthenticated.post('users/authenticate', credentials)
    .then((result) => {
      commit(types.LOGIN, result.data.token);
    })
    .catch((error) => {
      dispatch('dialogs/addError', error.response.data.message, { root: true });
      commit(types.LOGIN_FAILED);
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
