import Vue from 'vue';
import Vuex from 'vuex';
import createLogger from 'vuex/dist/logger';
import auth from '@/store/modules/auth';
import test from '@/store/modules/test';
import user from '@/store/modules/user';
import notes from '@/store/modules/notes';
import dialogs from '@/store/modules/dialogs';

Vue.use(Vuex);

const debug = process.env.NODE_ENV !== 'production';

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    test,
    auth,
    user,
    notes,
    dialogs,
  },
  plugins: debug ? [createLogger()] : [],
});
