<template>
  <v-timeline dense>
    <v-timeline-item v-for="note in notes" :key="note.id" :icon="note.typeId | typeicon" :color="note.typeId | typecolor" fill-dot small>
      <v-row justify="space-between">
        <v-col cols="7">
          <v-tooltip bottom v-if="note.description">
            <template v-slot:activator="{ on }">
              <span v-on="on" v-text="note.title"></span>
            </template>
            <MarkdownText :text="note.description" />
          </v-tooltip>
          <span v-else v-text="note.title"></span>
        </v-col>
        <v-col class="text-right" cols="5" v-text="$d(note.createdAt, 'long')"></v-col>
      </v-row>
    </v-timeline-item>
  </v-timeline>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins, Prop } from 'vue-property-decorator';
import NoteTypesMixin from '@/mixins/note-types';
import MarkdownText from '@/components/MarkdownText.vue';

@Component({
  components: { MarkdownText },
})
export default class TimelineSmall extends Mixins(NoteTypesMixin) {
  @Prop(Array) notes!: any[];
}
</script>
