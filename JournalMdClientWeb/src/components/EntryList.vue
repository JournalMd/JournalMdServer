<template>
  <v-row v-if="!small">
    <v-col v-for="type in noteTypes" :key="type.key" cols="12" sm="12" md="3">
      <v-card class="mx-auto">
        <v-list-item three-line>
          <v-list-item-content>
            <v-list-item-title class="headline" v-text="type.title"></v-list-item-title>
            <v-list-item-subtitle v-text="type.description"></v-list-item-subtitle>
          </v-list-item-content>

          <v-list-item-avatar tile size="96">
            <v-icon size="96">{{ type.name | typeicon }}</v-icon>
          </v-list-item-avatar>
        </v-list-item>

        <v-card-actions>
          <v-btn color="secondary" text @click="createNote(type.id)">{{ $t('general.create') }}</v-btn>
        </v-card-actions>
      </v-card>
    </v-col>
  </v-row>
  <v-row v-else>
    <v-col cols="12">
      <v-icon v-for="type in noteTypes" :key="type.key" class="mr-4" size="24" @click="createNote(type.id)" :color="type.name | typecolor">
        {{ type.name | typeicon }}
      </v-icon>
    </v-col>
  </v-row>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop, Mixins } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';
import NoteTypesMixin from '@/mixins/note-types';

const notesModule = namespace('notes');
const dialogsModule = namespace('dialogs');

@Component
export default class EntryList extends Mixins(NoteTypesMixin) {
  @Prop(Boolean) small!: boolean;

  @notesModule.State noteTypes: any;

  @notesModule.Getter getTypeById: any;

  @dialogsModule.Action('createNote') createNoteAction!: any;

  createNote(typeId: number) {
    const type = this.getTypeById(typeId);
    this.createNoteAction(type);
  }
}
</script>
