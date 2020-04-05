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
            <v-form ref="upform">
              <v-row>
                <v-col cols="12" sm="6" md="6">
                  <v-text-field label="First name" v-model="user.firstName"></v-text-field>
                </v-col>
                <v-col cols="12" sm="6" md="6">
                  <v-text-field label="Last name" v-model="user.lastName"></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12">
                  <v-menu v-model="datemenu" :close-on-content-click="false" :nudge-right="40"
                          transition="scale-transition" offset-y min-width="290px">
                    <template v-slot:activator="{ on }">
                      <v-text-field v-model="user.dateOfBirth" label="Date of birth" readonly v-on="on" />
                    </template>
                    <v-date-picker v-model="user.dateOfBirth" @input="datemenu = false"></v-date-picker>
                  </v-menu>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12">
                  <v-select :items="gender" v-model="user.gender" label="Gender"></v-select>
                </v-col>
              </v-row>
            </v-form>
          </v-container>
          <small>*indicates required field</small>
          <template v-slot:actions>
            <v-card-actions>
              <v-spacer />
              <v-btn color="green darken-1" text @click="saveUserProfile()" :loading="loading">{{ $t('general.save') }}</v-btn>
            </v-card-actions>
          </template>
        </BaseCard>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <BaseCard>
          <v-container>
            <v-form ref="pwform">
              <v-row>
                <v-col cols="12">
                  <v-text-field label="Password*" type="password" required></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field label="New Password*" type="password" required></v-text-field>
                </v-col>
              </v-row>
            </v-form>
          </v-container>
          <small>*indicates required field</small>
          <template v-slot:actions>
            <v-card-actions>
              <v-spacer />
              <v-btn color="green darken-1" text @click="savePassword()" :loading="loading">{{ $t('general.save') }}</v-btn>
            </v-card-actions>
          </template>
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
const dialogsModule = namespace('dialogs');

@Component({
  components: { BaseCard },
})
export default class UserProfile extends Vue {
  loading: boolean = false;

  datemenu: boolean = false;

  gender: { value: string, text: string }[] = [
    { value: 'm', text: 'Male' },
    { value: 'f', text: 'Female' },
  ];

  @userModule.State user!: User;

  // @userModule.Action('deleteNote') deleteNoteAction: any;

  @dialogsModule.Action('addMessage') addMessageAction!: any;

  saveUserProfile() {
    this.loading = true;
    /* this.deleteNoteAction(this.note.id).then(() => {
      this.loading = false;
      this.addMessageAction(this.$t('general.deleted'));
    }); */
  }

  savePassword() {
    this.loading = true;
    /* this.deleteNoteAction(this.note.id).then(() => {
      this.loading = false;
      this.addMessageAction(this.$t('general.deleted'));
    }); */
  }
}
</script>
