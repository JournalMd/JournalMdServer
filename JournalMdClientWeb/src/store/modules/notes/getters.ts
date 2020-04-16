import State from './state';

export const getNotesByType = (state: any) => (type: string) => {
  if (type === 'all') return state.notes;

  const foundType = state.noteTypes.find((sel: { name: string; }) => sel.name === type);
  if (foundType === undefined) return state.notes;

  const typeId = foundType.id;
  return state.notes.filter((sel: { typeId: string; }) => +sel.typeId === +typeId);
};

export const getNoteById = (state: any) => (id: number) => state.notes.find((sel: { id: string; }) => +sel.id === +id);

export const getTypeById = (state: any) => (id: number) => state.noteTypes.find((sel: { id: string; }) => +sel.id === +id);

export default {
  getNotesByType,
  getNoteById,
  getTypeById,
};
