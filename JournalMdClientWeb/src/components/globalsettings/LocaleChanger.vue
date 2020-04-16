<template>
  <div class="locale-changer">
    <v-select :items="langs" v-model="$root.$i18n.locale" :label="$t('general.language')" prepend-icon="mdi-flag"></v-select>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component
export default class LocaleChanger extends Vue {
  langs: { value: string, text: string }[] = [
    { value: 'en', text: 'English' },
    { value: 'de', text: 'Deutsch' },
  ];
}
</script>
