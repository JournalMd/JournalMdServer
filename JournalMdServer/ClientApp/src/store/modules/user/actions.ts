import { Commit } from 'vuex';
import { AxiosResponse } from 'axios';
import * as types from './mutation-types';
import { axiosAuthenticated, axiosUnauthenticated } from '@/api/api';

export const getUser = ({ commit } : { commit: Commit}) => {
  // const userURI = `${axiosUnauthenticated.}/users'`;

  const ids = [1, 2, 3, 4];
  const id = ids[Math.floor(Math.random() * ids.length)];

  axiosUnauthenticated.get('users', { params: { id } })
    .then((result) => {
      const user = {
        email: result.data[0].email,
        firstName: result.data[0].name.split(' ')[0],
        lastName: result.data[0].name.split(' ')[1],
      };
      console.log('get user result', result);
      commit(types.GET_USER, user);
    })
    .catch((error) => {
      console.log('Get User Request failed...', error); // TODO handle this
    });
  // .finally(() => this.loading = false)

  /*
  const user = {
    firstName: 'Susi',
    lastName: 'Musterfrau',
    email: 'susi@musterfrau.de',
  };

  commit(types.GET_USER, user);
  */
};

export default {
  getUser,
};
