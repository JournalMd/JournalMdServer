import {
  ADD_ERROR,
  ADD_MESSAGE,

  CLEAR_ERROR,
  CLEAR_MESSAGE,

  CREATE_NOTE,
  EDIT_NOTE,
  DELETE_NOTE,
} from './mutation-types';

export default {
  [ADD_ERROR](state: any, error: string) {
    state.error = error;
  },
  [ADD_MESSAGE](state: any, message: string) {
    state.message = message;
  },
  [CLEAR_ERROR](state: any) {
    state.error = '';
  },
  [CLEAR_MESSAGE](state: any) {
    state.message = '';
  },

  [CREATE_NOTE](state: any, type: any) {
    state.createNote = type;
  },
  [EDIT_NOTE](state: any, note: any) {
    state.editNote = note;
  },
  [DELETE_NOTE](state: any, note: any) {
    state.deleteNote = note;
  },
};
