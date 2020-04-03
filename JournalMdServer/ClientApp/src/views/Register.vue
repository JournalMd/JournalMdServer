<template>
  <v-container>
    Register
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Watch,
} from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
import BaseCard from '@/components/BaseCard.vue';

@Component({
  components: { BaseCard },
})
export default class Register extends Vue {
}
</script>
