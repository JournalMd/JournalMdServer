import { GET_USER } from './mutation-types';
import { User } from '../../../models/user';

export default {
  [GET_USER](state: any, user: User) {
    if (user === null) {
      state.email = null;
      state.firstName = null;
      state.lastName = null;
    } else {
      state.email = user.email;
      state.firstName = user.firstName;
      state.lastName = user.lastName;
    }
  },
};
