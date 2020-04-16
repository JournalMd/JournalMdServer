import { Commit } from 'vuex';
// import { AxiosResponse } from 'axios';
import * as types from './mutation-types';

export const addError = ({ commit } : { commit: Commit}, error: string) => {
  commit(types.ADD_ERROR, error);
};

export const addMessage = ({ commit } : { commit: Commit}, message: string) => {
  commit(types.ADD_MESSAGE, message);
};

export const clearError = ({ commit } : { commit: Commit}) => {
  commit(types.CLEAR_ERROR);
};

export const clearMessage = ({ commit } : { commit: Commit}) => {
  commit(types.CLEAR_MESSAGE);
};

export const createNote = ({ commit } : { commit: Commit}, type: any) => {
  commit(types.CREATE_NOTE, type);
};

export const editNote = ({ commit } : { commit: Commit}, note: any) => {
  commit(types.EDIT_NOTE, note);
};

export const deleteNote = ({ commit } : { commit: Commit}, note: any) => {
  commit(types.DELETE_NOTE, note);
};

export default {
  addError,
  addMessage,
  clearError,
  clearMessage,
  createNote,
  editNote,
  deleteNote,
};
