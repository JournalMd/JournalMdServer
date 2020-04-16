<template>
  <v-timeline dense>
    <v-timeline-item v-for="note in notes" :key="note.id" :icon="note.typeId | typeicon" :color="note.typeId | typecolor" fill-dot>
      <v-card>
        <v-card-title class="timelineTitle" :class="note.typeId | typecolor">
          <v-icon dark size="32" class="mr-4">{{ note.typeId | typeicon }}</v-icon>
          <h2 class="white--text font-weight-light" v-text="$d(note.createdAt, 'long')"></h2>
          <v-spacer></v-spacer>
          <v-icon dark size="32" class="mr-4">{{ note.mood | emoticonicon }}</v-icon>
          <v-menu offset-y>
            <template v-slot:activator="{ on }">
              <v-btn icon dark v-on="on"><v-icon>mdi-dots-vertical</v-icon></v-btn>
            </template>
            <v-list>
              <v-list-item @click="$emit('edit-note', note)">
                <v-list-item-title><v-icon>mdi-pencil</v-icon> {{ $t('general.edit') }}</v-list-item-title>
              </v-list-item>
              <v-list-item @click="$emit('delete-note', note)">
                <v-list-item-title><v-icon>mdi-delete</v-icon> {{ $t('general.delete') }}</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </v-card-title>
        <v-container fluid>
          <v-row>
            <v-col cols="12" class="font-weight-bold" v-text="note.title"></v-col>
          </v-row>
          <v-row>
            <v-col cols="12" class="pl-8">
              <MarkdownText :text="note.description" />
            </v-col>
            <v-col cols="12">
              <LabelList :labels="note.labels" />
            </v-col>
          </v-row>
        </v-container>
      </v-card>
    </v-timeline-item>
  </v-timeline>
</template>

<style scoped>
  .timelineTitle {
    padding: 8px;
  }
</style>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins, Prop } from 'vue-property-decorator';
import NoteTypesMixin from '@/mixins/note-types';
import EmoticonsMixin from '@/mixins/emoticons';
import MarkdownText from '@/components/MarkdownText.vue';
import LabelList from '@/components/LabelList.vue';

@Component({
  components: { MarkdownText, LabelList },
})
export default class Timeline extends Mixins(NoteTypesMixin, EmoticonsMixin) {
  @Prop(Array) notes!: any[];
}
</script>
