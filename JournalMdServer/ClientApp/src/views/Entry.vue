<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>{{ $t('views.entry') }}</h1>
      </v-col>
    </v-row>

    <EntryList />

  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import EntryList from '@/components/EntryList.vue';

@Component({
  components: { EntryList },
})
export default class Entry extends Vue {
}
</script>
