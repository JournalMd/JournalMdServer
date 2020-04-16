<template>
  <v-card class="mx-auto">
    <v-img v-if="title && image" :src="image" class="white--text align-end"
      gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="200px">
        <v-card-title v-text="title"></v-card-title>
    </v-img>
    <v-card-title v-else-if="title" v-text="title" class="headline"></v-card-title>

    <v-card-subtitle v-if="subtitle" v-text="subtitle"></v-card-subtitle>

    <v-card-text>
      <slot></slot>
    </v-card-text>

    <slot name="actions"></slot>
  </v-card>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

@Component
export default class BaseCard extends Vue {
  @Prop(String) image!: string;

  @Prop(String) title!: string;

  @Prop(String) subtitle!: string;
}
</script>
