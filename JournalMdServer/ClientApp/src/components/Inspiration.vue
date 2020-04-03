<template>
  <BaseCard
    :title="inspiration.title"
    :subtitle="inspiration.type"
    :image="img"
  >
    <MarkdownText :text="inspiration.description" />

    <template v-slot:actions>
      <v-card-actions>
        <v-btn color="secondary" text>TODO Use</v-btn>
      </v-card-actions>
    </template>
  </BaseCard>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins, Prop } from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
import BaseCard from '@/components/BaseCard.vue';
import MarkdownText from '@/components/MarkdownText.vue';

const imgurl = require('@/assets/blank-792125_960_720.webp');

const notesModule = namespace('notes');

@Component({
  components: { BaseCard, MarkdownText },
})
export default class Inspiration extends Vue {
  @notesModule.State inspirations: any;

  get inspiration() {
    if (this.inspirations.length < 1) {
      return { title: '', type: '', description: '' };
    }

    return this.inspirations[Math.floor(Math.random() * this.inspirations.length)];

    // TODO maybe show old entry as inspiration - e.g. today - one year ago
  }

  // eslint-disable-next-line class-methods-use-this
  get img() {
    return imgurl;
  }
}
</script>
