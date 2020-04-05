<template>
  <v-app>
    <Navbar v-if="!bootstrapping" />
    <v-content v-if="!bootstrapping">
      <GlobalDialogs ref="globalDialogs" />
      <router-view/>
    </v-content>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Navbar from '@/components/Navbar.vue';
import GlobalDialogs from '@/components/GlobalDialogs.vue';

@Component({
  components: { Navbar, GlobalDialogs },
})
export default class App extends Vue {
  bootstrapping: boolean = true;

  created() {
    this.$vuetify.theme.dark = false;
    this.$store.dispatch('auth/check').then(() => {
      this.$store.dispatch('notes/getNoteTypes'); // TODO: handle elsewhere
      this.$store.dispatch('notes/getLabels'); // TODO: handle elsewhere
      this.$store.dispatch('notes/getInspirations'); // TODO: handle elsewhere
      this.$store.dispatch('notes/getNotes'); // TODO: handle elsewhere

      this.bootstrapping = false;
    });
  }
}
</script>
