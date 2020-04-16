<template>
  <div class="theme-switch">
    <v-switch v-model="toggleDarkTheme" color="grey"></v-switch>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class ThemeSwitch extends Vue {
  get toggleDarkTheme() {
    return this.$vuetify.theme.dark;
  }

  set toggleDarkTheme(val: boolean) {
    this.$vuetify.theme.dark = val;
  }
}
</script>
