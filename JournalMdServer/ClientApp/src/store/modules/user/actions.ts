import { Commit, Dispatch } from 'vuex';
import { AxiosResponse } from 'axios';
import { axiosAuthenticated, axiosUnauthenticated } from '../../../api/api';
import * as types from './mutation-types';

export const getUser = ({ commit, dispatch }: { commit: Commit, dispatch: Dispatch }) => {
  axiosAuthenticated.get('users', {})
    .then((result) => {
      console.log('get user result', result);
      const user = {
        email: result.data.username,
        firstName: result.data.firstName,
        lastName: result.data.lastName,
      };
      commit(types.GET_USER, user);
    })
    .catch((error) => {
      console.log('Get User Request failed...', error);
      commit(types.GET_USER, null);
      dispatch('dialogs/addError', error.response.data.message, { root: true });
    });
};

export default {
  getUser,
};
