<template>
  <v-dialog v-model="dialog" max-width="600px">
    <v-card>
      <v-card-title>
        <span class="headline">{{ $t('general.delete') }}</span>
      </v-card-title>
      <v-card-text>
        {{ note ? note.title : '' }}
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="darken-1" text @click="dialog = false">{{ $t('general.close') }}</v-btn>
        <v-btn color="red" text @click="dialog = false; deleteNote()" :loading="loading">{{ $t('general.delete') }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Watch,
} from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';

const notesModule = namespace('notes');
const dialogsModule = namespace('dialogs');

@Component
export default class DeleteDialog extends Vue {
  dialog: boolean = false;

  loading: boolean = false;

  get note() {
    this.loading = false;
    this.dialog = this.$store.state.dialogs.deleteNote != null;
    return this.$store.state.dialogs.deleteNote;
  }

  @notesModule.Action('deleteNote') deleteNoteAction: any;

  @dialogsModule.Action('deleteNote') dialogsDeleteNoteAction: any;

  @dialogsModule.Action('addMessage') addMessageAction!: any;

  deleteNote() {
    this.loading = true;
    this.deleteNoteAction(this.note.id).then(() => {
      this.loading = false;
      this.addMessageAction(this.$t('general.deleted'));
    });
  }

  @Watch('dialog')
  onDialogChange(newValue: boolean, oldValue: boolean) {
    if (this.$store.state.dialogs.deleteNote != null && newValue === false) {
      this.dialogsDeleteNoteAction(null); // reset "current" selection to null to allow reopening the dialog
    }
  }
}
</script>
