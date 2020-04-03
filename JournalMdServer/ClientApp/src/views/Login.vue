<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <BaseCard :title="$t('views.login')">
          <v-btn color="green darken-1" text @click="login()" :loading="loading">{{ $t('views.login') }}</v-btn>
        </BaseCard>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Watch,
} from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
import BaseCard from '@/components/BaseCard.vue';

const authModule = namespace('auth');

@Component({
  components: { BaseCard },
})
export default class Login extends Vue {
  loading: boolean = false;

  @authModule.Action('login') loginAction: any;

  login() {
    this.loading = true;
    this.loginAction({ username: '1', password: '12345678' }).then(() => {
      this.loading = false;
    });
  }
}
</script>
