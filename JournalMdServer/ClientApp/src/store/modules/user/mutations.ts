import { GET_USER } from './mutation-types';
import { User } from '@/models/user';

export default {
  [GET_USER](state: any, user: User) {
    state.email = user.email;
    state.firstName = user.firstName;
    state.lastName = user.lastName;
  },
};
