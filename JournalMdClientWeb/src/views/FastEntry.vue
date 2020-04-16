<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>{{ $t('views.fastEntry') }}</h1>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <BaseCard>
          Hier ist eine super intelligente Eingabemaske. Bald... (TODO).
        </BaseCard>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import BaseCard from '@/components/BaseCard.vue';

@Component({
  components: { BaseCard },
})
export default class FastEntry extends Vue {
}
</script>
