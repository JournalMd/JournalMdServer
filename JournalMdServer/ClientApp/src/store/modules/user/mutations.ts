import { GET_USER, GET_USER_FAILED } from './mutation-types';
import { User } from '../../../models/user';

export default {
  [GET_USER](state: any, user: User) {
    state.email = user.email;
    state.firstName = user.firstName;
    state.lastName = user.lastName;
  },

  [GET_USER_FAILED](state: any) {
    state.email = null;
    state.firstName = null;
    state.lastName = null;
  },
};
