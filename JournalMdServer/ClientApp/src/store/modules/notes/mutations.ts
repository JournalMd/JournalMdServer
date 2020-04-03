import {
  GET_NOTE_TYPES,
  GET_LABELS,
  GET_INSPIRATIONS,
  GET_NOTES,
  CREATE_NOTE,
  EDIT_NOTE,
  DELETE_NOTE,
} from './mutation-types';
// import { User } from '@/models/user';

export default {
  [GET_NOTE_TYPES](state: any, noteTypes: any[]) {
    state.noteTypes = noteTypes;
  },
  [GET_LABELS](state: any, labels: any[]) {
    state.labels = labels;
  },
  [GET_INSPIRATIONS](state: any, inspirations: any[]) {
    state.inspirations = inspirations;
  },
  [GET_NOTES](state: any, notes: any[]) {
    state.notes = notes;
  },
  [CREATE_NOTE](state: any, note: any) {
    /* eslint-disable no-param-reassign */
    note.id = state.notes.length + 1;
    state.notes = [note, ...state.notes];
  },
  [EDIT_NOTE](state: any, note: any) {
    /* state.notes = [
      ...state.notes.filter((fNote: { id: any; }) => fNote.id !== note.id),
      note,
    ]; */
    // keep order
    state.notes = [...state.notes.map((fNote: { id: any; }) => (fNote.id !== note.id ? fNote : { ...fNote, ...note }))];
  },
  [DELETE_NOTE](state: any, id: number) {
    const index = state.notes.findIndex((i: { id: number; }) => i.id === id);
    state.notes.splice(index, 1);
  },
};
