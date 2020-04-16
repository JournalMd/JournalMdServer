import { Commit } from 'vuex';
// import { AxiosResponse } from 'axios';
import * as types from './mutation-types';
// import { axiosAuthenticated, axiosUnauthenticated } from '@/api/api';

// TODO
// Find a better name than note
// getNoteType from fixed backend service
// getLabels " => categories - labels => tags
// all "notes" stay in one large object - api is called by specific type => generic
// stronly typed entities is mainly base entity from db (=id) ;-)

/* eslint-disable object-curly-newline, max-len, object-property-newline */ // Allow test data onliner
export const getNoteTypes = ({ commit } : { commit: Commit}) => {
  const noteTypes: any[] = [ // + noteFields // TODO strongly type
    { id: 1, order: 1, name: 'note', title: 'Note', description: 'Write a simple note.', owner: null, key: 'note', fields: [] },
    { id: 2, order: 2, name: 'task', title: 'Task', description: 'A task you must complete.', owner: null, key: 'task', fields: [
      { id: 1, order: 1, name: 'completed', title: 'Completed', description: 'Is it done?', type: 'boolean', showInViews: true },
      { id: 5, order: 2, name: 'due', title: 'Due', description: 'When is it due?', type: 'date', showInViews: true },
    ] },
    { id: 3, order: 3, name: 'goal', title: 'Goal', description: 'A goal you want to achieve.', owner: null, key: 'goal', fields: [
      { id: 2, order: 1, name: 'achieved', title: 'Achieved', description: 'Did you make it?', type: 'boolean', showInViews: true },
    ] },
    { id: 4, order: 4, name: 'journal', title: 'Journal', description: 'Summarize your day or write down your thoughts.', owner: null, key: 'journal', fields: [] },
    { id: 5, order: 5, name: 'activity', title: 'Activity', description: 'Something you\'ve done.', owner: null, key: 'activity', fields: [] },
    { id: 6, order: 6, name: 'habit', title: 'Habit', description: 'Record your habits.', owner: null, key: 'habit', fields: [] },
    { id: 7, order: 7, name: 'routine', title: 'Routine', description: 'Write down what you want to do every day.', owner: null, key: 'routine', fields: [] },
    { id: 8, order: 8, name: 'weight', title: 'Weight', description: 'Track your weight.', owner: null, key: 'weight', fields: [
      { id: 3, order: 1, name: 'height', title: 'Height', description: 'Your height in cm.', type: 'number', showInViews: false },
      { id: 4, order: 2, name: 'weight', title: 'Weight', description: 'Your weight in kg.', type: 'number', showInViews: true },
    ] },
  ];

  commit(types.GET_NOTE_TYPES, noteTypes);
};

export const getLabels = ({ commit } : { commit: Commit}) => {
  const labels: any[] = [ // TODO strongly type
    // Auto Tagger weekday
    { id: 1, category: 'weekday', name: 'weekday_monday', title: 'Monday', owner: null, parent: null },
    { id: 2, category: 'weekday', name: 'weekday_tuesday', title: 'Tuesday', owner: null, parent: null },
    { id: 3, category: 'weekday', name: 'weekday_wednesday', title: 'Wednesday', owner: null, parent: null },
    { id: 4, category: 'weekday', name: 'weekday_thursday', title: 'Thursday', owner: null, parent: null },
    { id: 5, category: 'weekday', name: 'weekday_friday', title: 'Friday', owner: null, parent: null },
    { id: 6, category: 'weekday', name: 'weekday_saturday', title: 'Saturday', owner: null, parent: null },
    { id: 7, category: 'weekday', name: 'weekday_sunday', title: 'Sunday', owner: null, parent: null },
    // Auto Tagger time
    { id: 8, category: 'time', name: 'time_today', title: 'Today', owner: null, parent: null },
    { id: 9, category: 'time', name: 'time_week', title: 'Week', owner: null, parent: null },
    { id: 10, category: 'time', name: 'time_month', title: 'Month', owner: null, parent: null },
    { id: 11, category: 'time', name: 'time_year', title: 'Year', owner: null, parent: null },
    { id: 12, category: 'time', name: 'time_life', title: 'Life', owner: null, parent: null },
    // categorie
    { id: 13, category: 'categorie', name: 'categorie_quote', title: 'Quote', owner: null, parent: null },
    { id: 14, category: 'categorie', name: 'categorie_shopping_list', title: 'Shopping_list', owner: null, parent: null },
    { id: 15, category: 'categorie', name: 'categorie_xxx', title: 'xxx', owner: null, parent: null },
    { id: 16, category: 'categorie', name: 'categorie_yyy', title: 'yyy', owner: null, parent: null },
    { id: 17, category: 'categorie', name: 'categorie_zzz', title: 'zzz', owner: null, parent: null },
    // activity
    { id: 18, category: 'activity', name: 'activity_weight_training', title: 'Weight_training', owner: null, parent: null },
    { id: 19, category: 'activity', name: 'activity_running', title: 'Running', owner: null, parent: null },
    { id: 20, category: 'activity', name: 'activity_dancing', title: 'Dancing', owner: null, parent: null },
    // user
    { id: 21, category: 'categorie', name: 'user_user_a', title: 'User_a', owner: 1, parent: null },
    { id: 22, category: 'categorie', name: 'user_user_b', title: 'User_b', owner: 1, parent: null },
    { id: 23, category: 'categorie', name: 'user_user_c', title: 'User_c', owner: 1, parent: null },
  ];

  commit(types.GET_LABELS, labels);
};

export const getInspirations = ({ commit } : { commit: Commit}) => {
  const inspirations: any[] = [ // TODO strongly type
    { id: 1, type: 'journal', title: 'Journal Inspiration #1', description: 'Write one.' },
    { id: 2, type: 'journal', title: 'Journal Inspiration #2', description: 'Write every day.' },
    { id: 3, type: 'journal', title: 'Journal Inspiration #3', description: 'TODO ich muss mir echt mehr Beispiele einfallen lassen...' },
  ];

  commit(types.GET_INSPIRATIONS, inspirations);
};

export const getNotes = ({ commit } : { commit: Commit}) => {
  const notes: any[] = [ // + noteFieldValues // TODO strongly type
    { id: 1, typeId: 1, title: 'Note 1', description: 'Zombie ipsum reversus ab viral inferno, nam rick grimes malum cerebro. De carne lumbering animata corpora quaeritis. Summus brains sit​​, morbo vel maleficia?', createdAt: new Date(2020, 0, 1, 11, 11), mood: 5, labels: [1, 8, 21, 22, 23] },
    { id: 2, typeId: 1, title: 'Note 2', description: 'De apocalypsi gorger omero undead survivor dictum mauris.', createdAt: new Date(2020, 0, 2, 12, 12), mood: 4, labels: [] },
    { id: 3, typeId: 1, title: 'Note 3 The voodoo sacerdos flesh eater, suscitat mortuos comedere carnem virus.', description: 'Hi mindless mortuis soulless creaturas, imo evil stalking monstra adventus resi dentevil vultus comedat cerebella viventium. Qui animated corpse, cricket bat max brucks terribilem incessu zomby.', createdAt: new Date(2020, 0, 3, 10, 0), mood: null, labels: [] },
    { id: 4, typeId: 1, title: 'Note 4 Really long ding dong', description: '', createdAt: new Date(2020, 0, 3, 14, 0), mood: 5, labels: [] },

    { id: 5, typeId: 2, title: 'Task 1', description: 'Kekse backen.', createdAt: new Date(2020, 0, 1, 11, 12), mood: 4, labels: [1, 8],
      fields: { completed: { id: 1, fieldId: 1, value: 'true' }, due: { id: 11, fieldId: 5, value: '2020-04-04' } } },
    { id: 6, typeId: 2, title: 'Task 2', description: 'Kekse essen...', createdAt: new Date(2020, 0, 1, 11, 15), mood: 5, labels: [1, 8],
      fields: { completed: { id: 2, fieldId: 1, value: 'false' }, due: { id: 12, fieldId: 5, value: '2020-05-05' } } },

    { id: 7, typeId: 3, title: 'Goal 1 Abnehmen', description: '', createdAt: new Date(2020, 0, 1, 11, 14), mood: 1, labels: [1, 8],
      fields: { achieved: { id: 3, fieldId: 2, value: 'true' } } },
    { id: 8, typeId: 3, title: 'Goal 2 Noch mehr abnehmen', description: 'Viel...', createdAt: new Date(2020, 0, 1, 11, 16), mood: 2, labels: [1, 8],
      fields: { achieved: { id: 4, fieldId: 2, value: 'false' } } },

    { id: 9, typeId: 4, title: 'Journal', description: '', createdAt: new Date(2020, 0, 1, 11, 14), mood: 5, labels: [1, 8] },
    { id: 10, typeId: 4, title: 'Noch ein Journal', description: '# Markdown\n\n**dfff**\n\nsdfsdg\n\n* a\n* [ ] b', createdAt: new Date(2020, 0, 2, 1, 1), mood: null, labels: [] },

    { id: 11, typeId: 5, title: 'Activity 1', description: 'Kleine Runde', createdAt: new Date(2020, 0, 1, 11, 22), mood: null, labels: [1, 8, 19] },

    { id: 12, typeId: 6, title: 'Habit 1', description: '', createdAt: new Date(2020, 0, 1, 11, 22), mood: null, labels: [1, 8] },
    { id: 13, typeId: 6, title: 'Habit 2', description: '', createdAt: new Date(2019, 7, 7, 11, 22), mood: 5, labels: [] },

    { id: 14, typeId: 7, title: 'Routine 1', description: '', createdAt: new Date(2020, 0, 2, 11, 22), mood: 2, labels: [] },
    { id: 15, typeId: 7, title: 'Routine 2', description: '', createdAt: new Date(2019, 8, 8, 11, 22), mood: null, labels: [] },

    { id: 16, typeId: 8, title: 'Weight 1', description: '', createdAt: new Date(2019, 2, 1, 12, 12), mood: 5, labels: [],
      fields: { height: { id: 5, fieldId: 3, value: '192' }, weight: { id: 6, fieldId: 4, value: '81' } } },
    { id: 17, typeId: 8, title: 'Weight 2', description: '', createdAt: new Date(2019, 7, 2, 12, 14), mood: 2, labels: [],
      fields: { height: { id: 7, fieldId: 3, value: '192' }, weight: { id: 8, fieldId: 4, value: '86' } } },

    { id: 18, typeId: 3, title: 'Goal 3 Bucket List 1', description: 'Label Test Item', createdAt: new Date(2018, 0, 1, 11, 16), mood: 5, labels: [12],
      fields: { achieved: { id: 9, fieldId: 2, value: 'false' } } },
    { id: 19, typeId: 3, title: 'Goal 4 Bucket List 1', description: 'Label Test Item', createdAt: new Date(2018, 0, 1, 11, 18), mood: 5, labels: [12],
      fields: { achieved: { id: 10, fieldId: 2, value: 'false' } } },
  ];

  commit(types.GET_NOTES, notes);
};

export const createNote = ({ commit } : { commit: Commit}, note: any) => {
  // TODO api
  commit(types.CREATE_NOTE, note);
};

export const editNote = ({ commit } : { commit: Commit}, note: any) => {
  // TODO api
  commit(types.EDIT_NOTE, note);
};

export const deleteNote = ({ commit } : { commit: Commit}, id: number) => {
  // TODO api
  commit(types.DELETE_NOTE, id);
};

export default {
  getNoteTypes,
  getLabels,
  getInspirations,
  getNotes,
  createNote,
  editNote,
  deleteNote,
};
