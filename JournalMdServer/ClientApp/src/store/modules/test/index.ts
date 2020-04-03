import { Commit } from 'vuex';

export default {
  namespaced: true,
  state: {
    count: 0,
  },
  mutations: {
    increment(state: { count: number }) {
      state.count += 1;
    },
  },
  actions: {
    increment({ commit } : { commit: Commit}) {
      commit('increment');
    },
  },
};
