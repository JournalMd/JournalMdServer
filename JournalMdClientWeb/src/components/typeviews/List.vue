<template>
  <v-list two-line>
    <v-list-item-group>
      <template v-for="(note, index) in notes">
        <v-list-item :key="'li' + note.id">
          <template>
            <v-list-item-content>
              <v-list-item-title>
                <v-icon v-if="compact" :color="note.typeId | typecolor" style="vertical-align: baseline" size="16">
                  {{ note.typeId | typeicon }}
                </v-icon>
                {{ note.title }}
              </v-list-item-title>
              <v-list-item-subtitle v-if="!compact" class="ml-4 mt-2"><MarkdownText :text="note.description" /></v-list-item-subtitle>
              <v-list-item-subtitle v-if="!compact">
                <v-icon :color="note.typeId | typecolor">{{ note.typeId | typeicon }}</v-icon>
                <v-icon :color="note.mood | emoticoncolor">{{ note.mood | emoticonicon }}</v-icon>
                <LabelList :labels="note.labels" />
              </v-list-item-subtitle>
            </v-list-item-content>

            <v-list-item-action v-if="!compact">
              <v-list-item-action-text v-text="$d(note.createdAt, 'long')"></v-list-item-action-text>
              <v-menu offset-y>
                <template v-slot:activator="{ on }">
                  <v-btn icon v-on="on"><v-icon>mdi-dots-vertical</v-icon></v-btn>
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
            </v-list-item-action>
          </template>
        </v-list-item>

        <v-divider v-if="index + 1 < notes.length" :key="index"></v-divider>
      </template>
    </v-list-item-group>
  </v-list>
</template>

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
export default class List extends Mixins(NoteTypesMixin, EmoticonsMixin) {
  @Prop(Array) notes!: any[];

  @Prop(Boolean) compact!: boolean;
}
</script>
