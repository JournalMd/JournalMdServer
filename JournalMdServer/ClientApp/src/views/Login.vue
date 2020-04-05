<template>
  <v-container class="fill-height" fluid>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="4">
        <v-card class="elevation-12">
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>{{ $t('views.login') }}</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form>
              <v-text-field v-model="email" :label="$t('fields.email')" name="email" prepend-icon="mdi-email" type="text" />
              <v-text-field v-model="password" id="password" :label="$t('fields.password')"
                            name="password" prepend-icon="mdi-lock" type="password" />
              <LocaleChanger class="maxWidth" />
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-btn color="secondary" to="/register" text>{{ $t('views.register') }}</v-btn>
            <v-spacer />
            <v-btn color="primary" @click="login()" :loading="loading">{{ $t('views.login') }}</v-btn>
          </v-card-actions>
        </v-card>
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
import LocaleChanger from '@/components/globalsettings/LocaleChanger.vue';

const authModule = namespace('auth');

@Component({
  components: { LocaleChanger },
})
export default class Login extends Vue {
  loading: boolean = false;

  email: string = '1';

  password: string = '12345678';

  @authModule.Action('login') loginAction: any;

  @authModule.State authenticated: any;

  mounted() {
    if (this.authenticated) {
      this.$router.push('/');
    }
  }

  login() {
    this.loading = true;
    this.loginAction({ username: this.email, password: this.password }).then(() => {
      this.loading = false;
    });
  }
}
</script>
