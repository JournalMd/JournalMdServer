import { Commit, Dispatch } from 'vuex';
import { AxiosResponse } from 'axios';
import { axiosAuthenticated, axiosUnauthenticated } from '../../../api/api';
import { User } from '../../../models/user';
import * as types from './mutation-types';

export const getUser = ({ commit, dispatch }: { commit: Commit, dispatch: Dispatch }) => {
  axiosAuthenticated.get('users', {})
    .then((result) => {
      const user: User = {
        email: result.data.username,
        firstName: result.data.firstName,
        lastName: result.data.lastName,
      };
      commit(types.GET_USER, user);
    })
    .catch((error) => {
      dispatch('dialogs/addError', error.response.data.message, { root: true });
      commit(types.GET_USER_FAILED);
    });
};

export default {
  getUser,
};
