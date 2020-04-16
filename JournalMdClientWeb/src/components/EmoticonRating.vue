<template>
  <v-rating v-model="currentMood" @input="input">
    <template v-slot:item="props">
      <v-icon
        :color="props.isFilled ? genColor(props.index) : 'grey lighten-1'"
        large
        @click="props.click"
      >
        {{ props.index + 1 | emoticonicon }}
      </v-icon>
    </template>
  </v-rating>
</template>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Emit,
} from 'vue-property-decorator';
import { mapState } from 'vuex';
import { State, namespace } from 'vuex-class';
import EmoticonsMixin from '@/mixins/emoticons';
import { emoticoncolor, emoticonicon } from '@/helper/emoticonConveters';

const notesModule = namespace('notes');

@Component
export default class EmoticonRating extends Mixins(EmoticonsMixin) {
  @Prop(Number) mood!: number;

  colors: string[] = ['red', 'orange', 'brown', 'green', 'blue'];

  currentMood: number = 1;

  mounted() {
    this.currentMood = this.mood;
  }

  // eslint-disable-next-line class-methods-use-this
  genColor(i: number) {
    return emoticoncolor(i + 1);
  }

  @Emit()
  input() {
    return this.currentMood;
  }
}
</script>
