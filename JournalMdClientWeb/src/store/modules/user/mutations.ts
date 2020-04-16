import { GET_USER, GET_USER_FAILED } from './mutation-types';
import { User } from '../../../models/user';

export default {
  [GET_USER](state: any, user: User) {
    state.user = user;
  },

  [GET_USER_FAILED](state: any) {
    state.user = null;
  },
};
