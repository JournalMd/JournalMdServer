<template>
  <v-card>
    <v-card-title v-if="!compact">
      <v-spacer></v-spacer>
      <v-text-field v-model="search" append-icon="mdi-magnify" label="Search" single-line hide-details />
    </v-card-title>
    <v-data-table
      :headers="headers"
      :items="notes"
      :items-per-page="10"
      :search="search"
      class="elevation-1"
      :hide-default-header="compact"
      :hide-default-footer="compact"
    >
      <template v-slot:item.typeId="{ item }">
        <v-chip :color="item.typeId | typecolor"><v-icon color="white">{{ item.typeId | typeicon }}</v-icon></v-chip>
      </template>
      <template v-slot:item.mood="{ item }">
        <v-icon :color="item.mood | emoticoncolor">{{ item.mood | emoticonicon }}</v-icon>
      </template>
      <template v-slot:item.createdAt="{ item }">
        {{ $d(item.createdAt, 'long') }}
      </template>
      <template v-slot:item.labels="{ item }">
        <LabelList :labels="item.labels" />
      </template>
      <template v-slot:item.action="{ item }">
        <v-icon small class="mr-2" @click="$emit('edit-note', item)">mdi-pencil</v-icon>
        <v-icon small @click="$emit('delete-note', item)">mdi-delete</v-icon>
      </template>
    </v-data-table>
  </v-card>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins, Prop } from 'vue-property-decorator';
import _ from 'lodash';
import NoteTypesMixin from '@/mixins/note-types';
import EmoticonsMixin from '@/mixins/emoticons';
import LabelList from '@/components/LabelList.vue';

@Component({
  components: { LabelList },
})
export default class Table extends Mixins(NoteTypesMixin, EmoticonsMixin) {
  @Prop(Array) notes!: any[];

  @Prop(Boolean) compact!: boolean;

  @Prop(Array) noteTypes!: any;

  @Prop(Array) labels!: [];

  search: string = '';

  get headers() {
    const baseHeader = [
      { text: 'Type', value: 'typeId' },
      { text: 'Title', value: 'title', sortable: false },
      // { text: 'Description', value: 'description', sortable: false }, // <MarkdownText :text="note.description" />
      { text: 'Mood', value: 'mood' },
      { text: 'Created', value: 'createdAt' },
      { text: 'Labels', value: 'labels', sortable: false },
    ];

    const actionsHeader = [
      { text: this.$t('general.actions'), value: 'action', sortable: false },
    ];

    // f multiple node types are shown then only return the base header without fields
    const noteTypes = this.notes.map(sel => sel.typeId);
    if (_.uniq(noteTypes).length > 1) {
      return [...baseHeader, ...actionsHeader];
    }

    // Create headers for fields of single note type
    const fieldsHeader = this.noteTypes
      .find((sel: any) => sel.id === noteTypes[0])
      .fields.filter((sel: any) => sel.showInViews).map((sel: any) => ({
        text: sel.title,
        value: `fields.${sel.name}.value`,
      }));

    return [...baseHeader, ...fieldsHeader, ...actionsHeader];
  }
}
</script>
