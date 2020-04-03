<template>
  <span>
    <v-chip v-for="labelId in labels" :key="labelId" class="ma-1" x-small v-text="getName(labelId)"></v-chip>
  </span>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins, Prop } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';

const notesModule = namespace('notes');

@Component
export default class LabelList extends Vue {
  @Prop(Array) labels!: [];

  @notesModule.State('labels') stateLabels: any;

  getName(id: number) {
    return this.stateLabels.find((sel: any) => +sel.id === +id).title;
  }
}
</script>
