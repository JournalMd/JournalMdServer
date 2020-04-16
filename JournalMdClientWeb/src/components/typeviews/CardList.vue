<template>
  <v-container dense>
    <v-row>
      <v-col v-for="note in notes" :key="note.id" cols="12" md="4">
        <v-card>
          <v-img src="@/assets/blank-792125_960_720.webp" class="white--text align-end"
            gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="200px">
            <v-card-title v-text="note.title"></v-card-title>
          </v-img>

          <v-card-actions>
            <v-btn icon>
              <v-icon :color="note.typeId | typecolor">{{ note.typeId | typeicon }}</v-icon>
            </v-btn>
            <v-btn icon>
              <v-icon :color="note.mood | emoticoncolor">{{ note.mood | emoticonicon }}</v-icon>
            </v-btn>
            <v-spacer></v-spacer>
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
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Emit,
} from 'vue-property-decorator';
import NoteTypesMixin from '@/mixins/note-types';
import EmoticonsMixin from '@/mixins/emoticons';

@Component
export default class CardList extends Mixins(NoteTypesMixin, EmoticonsMixin) {
  @Prop(Array) notes!: any[];
}
</script>
