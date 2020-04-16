<template>
  <v-dialog v-model="dialog" persistent max-width="1200px">
    <v-card>
      <v-card-title>
        <span class="headline">
          <v-icon size="24" style="vertical-align: baseline" :color="type.name | typecolor">{{ type.name | typeicon }}</v-icon>
          {{ $t(`general.${mode}`) }}
        </span>
      </v-card-title>
      <v-card-text>
        <v-form ref="cuform">
          <v-container>
            <v-row dense>
              <v-col cols="12">
                <v-text-field v-model="note.title" :label="$t('fields.title')" filled
                  :rules="[rules.required, rules.min3]"></v-text-field>
              </v-col>

              <v-col cols="12">
                <vue-easymde v-model="note.description" ref="markdownEditor" :configs="configs" />
              </v-col>

              <v-col cols="12">
                <v-label dense>Mood</v-label>
                <EmoticonRating :mood="note.mood" @input="note.mood = $event" filled />
              </v-col>

              <v-col cols="12">
                <v-autocomplete
                  v-model="note.labels"
                  :items="labels"
                  item-text="title"
                  item-value="id"
                  label="Label"
                  multiple
                  chips
                  small-chips
                  filled
                ></v-autocomplete>
              </v-col>

              <v-col cols="12" v-for="field in type.fields" :key="field.id">
                <v-text-field v-if="field.type === 'number'" v-model="note.fields[field.name].value" :label="field.title"
                              filled :rules="[rules.number, rules.required]"></v-text-field>
                <v-checkbox v-if="field.type === 'boolean'" v-model="note.fields[field.name].value" :label="field.title"></v-checkbox>
                <v-menu v-if="field.type === 'date'" v-model="datemenu" :close-on-content-click="false" :nudge-right="40"
                        transition="scale-transition" offset-y min-width="290px">
                  <template v-slot:activator="{ on }">
                    <v-text-field v-model="note.fields[field.name].value" label="Date" prepend-icon="mdi-calendar" readonly v-on="on">
                    </v-text-field>
                  </template>
                  <v-date-picker v-model="note.fields[field.name].value" @input="datemenu = false"></v-date-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="darken-1" text @click="dialog = false">{{ $t('general.close') }}</v-btn>
        <v-btn color="green darken-1" text @click="submit" :loading="loading">{{ $t(`general.${mode}`) }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<style>
  @import "~easymde/dist/easymde.min.css";
</style>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Watch,
} from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
import _ from 'lodash';
// eslint-disable-next-line import/extensions
import VueEasymde from 'vue-easymde';
import NoteTypesMixin from '@/mixins/note-types';
import EmoticonRating from '@/components/EmoticonRating.vue';

const notesModule = namespace('notes');
const dialogsModule = namespace('dialogs');

@Component({
  components: { EmoticonRating, VueEasymde },
})
export default class CreateEditDialog extends Mixins(NoteTypesMixin) {
  dialog: boolean = false;

  note: any = {};

  type: any = {};

  loading: boolean = false;

  datemenu: boolean = false;

  @notesModule.State labels: any;

  configs: {} = {
    spellChecker: false, // disable spell check
    uploadImage: false,
    toolbar: ['bold', 'italic', 'strikethrough', 'heading', '|', 'quote', 'unordered-list', 'ordered-list', '|',
      'link', /* 'image', */ '|', 'preview', 'side-by-side', 'fullscreen', '|', 'guide'],
  }

  rules: any = {
    required: (value: any) => !!value || 'Required.',
    min3: (value: any) => value.length >= 3 || 'Min 3 characters.',
    number: (value: any) => _.isFinite(_.toNumber(value.replace(',', '.'))) || 'Not a valid number',
  };

  @notesModule.Getter getTypeById: any;

  get mode() {
    if (this.$store.state.dialogs.editNote !== null) {
      this.note = this.$store.state.dialogs.editNote;
      this.type = this.getTypeById(this.note.typeId);
    } else if (this.$store.state.dialogs.createNote !== null) {
      this.type = this.$store.state.dialogs.createNote;

      // create empty fields
      const noteFields = this.type.fields.reduce((map: any, field: any) => {
        const newMap = map;
        newMap[field.name] = {
          id: -1,
          fieldId: field.id,
          value: '',
        };
        return newMap;
      }, {});

      this.note = {
        title: '',
        description: '',
        mood: 3,
        typeId: this.$store.state.dialogs.createNote.id,
        fields: noteFields,
      };
    } // else - won't show anyway

    if (this.$refs.cuform != null) {
      const cuForm = this.$refs.cuform; // reset validation in case you came from edit and required fields are now empty
      (cuForm as any).resetValidation(); // super uggly typescript workaround
    }

    this.loading = false;
    this.dialog = this.$store.state.dialogs.editNote !== null || this.$store.state.dialogs.createNote !== null;
    return this.$store.state.dialogs.editNote != null ? 'edit' : 'create';
  }

  @notesModule.Action('createNote') createNoteAction: any;

  @notesModule.Action('editNote') editNoteAction: any;

  @dialogsModule.Action('addMessage') addMessageAction!: any;

  submit() {
    const cuForm = this.$refs.cuform; // super uggly typescript workaround
    if (cuForm != null && (cuForm as any).validate()) {
      this.loading = true;
      if (this.$store.state.dialogs.createNote != null) {
        this.createNoteAction(this.note).then(() => {
          this.loading = false;
          this.dialog = false;
          this.addMessageAction(this.$t('general.created'));
        });
      } else {
        this.editNoteAction(this.note).then(() => {
          this.loading = false;
          this.dialog = false;
          this.addMessageAction(this.$t('general.edited'));
        });
      }
    }
  }

  @dialogsModule.Action('createNote') dialogsCreateNoteAction: any;

  @dialogsModule.Action('editNote') dialogsEditNoteAction: any;

  @Watch('dialog')
  onDialogChange(newValue: boolean, oldValue: boolean) {
    if (this.$store.state.dialogs.createNote != null && newValue === false) {
      this.dialogsCreateNoteAction(null); // reset "current" selection to null to allow reopening the dialog
    }
    if (this.$store.state.dialogs.editNote != null && newValue === false) {
      this.dialogsEditNoteAction(null); // reset "current" selection to null to allow reopening the dialog
    }
  }
}
</script>
