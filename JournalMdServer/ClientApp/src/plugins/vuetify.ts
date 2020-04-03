import Vue from 'vue';
import Vuetify from 'vuetify/lib';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    // dark: false,
    themes: {
      light: {
        primary: '#4caf50',
        secondary: '#03a9f4',
        accent: '#00bcd4',
        error: '#f44336',
        warning: '#ff9800',
        info: '#2196f3',
        success: '#8bc34a',
      },
      dark: {
        primary: '#03a9f4',
        secondary: '#4caf50',
        accent: '#00bcd4',
        error: '#f44336',
        warning: '#ff9800',
        info: '#2196f3',
        success: '#8bc34a',
      },
    },
  },
});
