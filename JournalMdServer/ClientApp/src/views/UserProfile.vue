<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>{{ $t('views.profile') }}</h1>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <BaseCard>
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="64">
                <v-text-field label="First name" v-model="firstName" required></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="6">
                <v-text-field label="Last name" v-model="lastName" required></v-text-field>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </BaseCard>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <BaseCard>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field label="Password*" type="password" required></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field label="New Password*" type="password" required></v-text-field>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </BaseCard>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';
import BaseCard from '@/components/BaseCard.vue';
import { User } from '@/models/user';

const userModule = namespace('user');

@Component({
  components: { BaseCard },
})
export default class UserProfile extends Vue {
  @userModule.State firstName!: string;

  @userModule.State lastName!: string;
}
</script>
