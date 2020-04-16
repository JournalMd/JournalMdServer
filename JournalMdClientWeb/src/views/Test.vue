<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>{{ $t('welcome') }}</h1>
      </v-col>
      <v-col cols="12">
        <BaseCard>
          Sinnfreier Text usw. ...
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard title="Icons">
          <v-icon left>mdi-email</v-icon>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard title="env">
          <div>{{ nodeEnv }}</div>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard title="i18n">
          <div><LocaleChanger class="maxWidth" /></div>
          <div><strong>Welcome:</strong> {{ $t('test.welcome') }}</div>
          <div><strong>{0} Text:</strong> {{ $t('test.hello', ['hello']) }}</div>
          <div><strong>Count 0:</strong> {{ $tc('test.apple', 0) }}</div>
          <div><strong>Count 1:</strong> {{ $tc('test.apple', 1) }}</div>
          <div><strong>Count 10:</strong> {{ $tc('test.apple', 10, { count: 10 }) }}</div>
          <div>
              <strong>Date Short:</strong> {{ $d(new Date(), 'short') }}<br />
              <strong>Date Long:</strong> {{ $d(new Date(), 'long') }}<br />
              <strong>Date 1.2:</strong> {{ $d(new Date(2020, 1, 1, 11, 11), 'long') }}
          </div>
          <div><strong>100666 currency:</strong> {{ $n(100666, 'currency') }}</div>
          <div><strong>100666.666 decimal:</strong> {{ $n(100666.666, 'decimal') }}</div>
          <div><strong>100666 percent:</strong> {{ $n(100666, 'percent') }}</div>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard :title="$t('general.darkTheme')">
          <div><ThemeSwitch class="maxWidth" /></div>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard title="Dialogs">
          <div class="mb-4">
            <v-btn @click="createNote" color="info">{{ $t('general.create') }}</v-btn>
            <v-btn @click="editNote" color="success" class="ml-2">{{ $t('general.edit') }}</v-btn>
            <v-btn @click="deleteNote" color="error" class="ml-2">{{ $t('general.delete') }}</v-btn>
          </div>
          <div>
            <v-btn @click="addError" color="error">Error</v-btn>
            <v-btn @click="addMessage" color="success" class="ml-2">Message</v-btn>
          </div>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard title="Router">
          <div><strong>$route:</strong> {{ this.$route.path }}, {{ this.$route.params }}, {{ this.$route.query }}</div>
          <div><strong>vuex-router-sync:</strong> {{ route.path }}, {{ route.params }}, {{ route.query }}</div>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <BaseCard title="Store">
          <div><strong>View count:</strong> {{ count }}, {{ countComputed }}</div>
          <div>{{ this.$store.state }}</div>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <v-img :src="require('../assets/logo.svg')" class="my-3" contain height="200"></v-img>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.maxWidth {
  max-width: 200px;
}
</style>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';
import LocaleChanger from '@/components/globalsettings/LocaleChanger.vue';
import ThemeSwitch from '@/components/globalsettings/ThemeSwitch.vue';
import BaseCard from '@/components/BaseCard.vue';
import CreateEditDialog from '@/components/notedialogs/CreateEditDialog.vue';
import DeleteDialog from '@/components/notedialogs/DeleteDialog.vue';

const testModule = namespace('test');
const dialogsModule = namespace('dialogs');

@Component({
  components: {
    LocaleChanger, ThemeSwitch, BaseCard, CreateEditDialog, DeleteDialog,
  },
})
export default class Test extends Vue {
  nodeEnv: string = process.env.NODE_ENV;

  @State route: any; // vuex-router-sync

  @testModule.State count!: number;

  @testModule.Action increment!: any;

  get countComputed() {
    return this.$store.state.test.count;
  }

  mounted() {
    this.increment();
    console.log('this.$store.state.test.count', this.$store.state.test.count);
  }

  @dialogsModule.Action('addError') addErrorAction!: any;

  addError() {
    this.addErrorAction('Oh nein. Du hast es kaputt gemacht :-(');
  }

  @dialogsModule.Action('addMessage') addMessageAction!: any;

  addMessage() {
    this.addMessageAction('Hab einen schönen Tag!');
  }

  @dialogsModule.Action('createNote') createNoteAction!: any;

  createNote() {
    this.createNoteAction(this.$store.state.notes.noteTypes[0]); // always use first type for tests...
  }

  @dialogsModule.Action('editNote') editNoteAction!: any;

  editNote() {
    this.editNoteAction(this.$store.state.notes.notes[0]); // always use first note for tests...
  }

  @dialogsModule.Action('deleteNote') deleteNoteAction!: any;

  deleteNote() {
    this.deleteNoteAction(this.$store.state.notes.notes[0]); // always use first note for tests...
  }
}
</script>
