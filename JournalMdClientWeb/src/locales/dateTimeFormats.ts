/* eslint-disable object-property-newline */
export default {
  en: {
    short: {
      year: 'numeric', month: 'short', day: 'numeric',
    },
    long: {
      year: 'numeric', month: 'short', day: 'numeric',
      /* weekday: 'short', */ hour: 'numeric', minute: 'numeric',
    },
  },
  de: {
    short: {
      year: 'numeric', month: '2-digit', day: '2-digit',
    },
    long: {
      year: 'numeric', month: '2-digit', day: '2-digit',
      /* weekday: 'short', */ hour: '2-digit', minute: '2-digit', hour12: false,
    },
  },
};
