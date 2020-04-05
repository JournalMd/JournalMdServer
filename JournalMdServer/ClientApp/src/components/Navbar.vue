<template>
  <nav>
    <v-navigation-drawer v-model="drawer" app v-if="authenticated">
      <template v-slot:prepend>
        <v-list-item two-line>
          <v-list-item-avatar>
            <img src="https://randomuser.me/api/portraits/women/81.jpg">
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>{{ user.firstName }} {{ user.lastName }}</v-list-item-title>
            <v-list-item-subtitle>Premium User</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </template>

      <v-list dense>
        <v-list-item v-for="link in links" :key="link.text" router :to="link.route">
          <v-list-item-action>
            <v-icon>{{ link.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ $t(link.text) }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>

      <v-divider></v-divider>

      <v-list dense>
        <v-list-group v-for="item in views" :key="item.text" v-model="item.active" :prepend-icon="item.action" no-action>
          <template v-slot:activator>
            <v-list-item-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{ $t(item.text) }}</v-list-item-title>
            </v-list-item-content>
          </template>
          <v-list-item v-for="subItem in item.items" :key="subItem.title" router :to="subItem.route">
            <v-list-item-action>
              <v-icon>{{ subItem.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{ $t(subItem.text) }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-group>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app color="primary" dark>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />

      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
        />
        <v-toolbar-title class="text-uppercase">JournalMd</v-toolbar-title>
      </div>

      <v-spacer></v-spacer>

      <!--<v-btn icon>
        <v-icon>mdi-heart</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>-->

      <v-menu left bottom transition="scale-transition" :close-on-content-click="false">
        <template v-slot:activator="{ on }">
          <v-btn icon v-on="on">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item>
            <v-list-item-action>
              <ThemeSwitch />
            </v-list-item-action>
            <v-list-item-title>{{ $t('general.darkTheme') }}</v-list-item-title>
          </v-list-item>
          <template v-if="authenticated">
            <v-list-item v-for="link in topSubMenu" :key="link.text" router :to="link.route">
              <v-list-item-action>
                <v-icon>{{ link.icon }}</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title>{{ $t(link.text) }}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </template>
        </v-list>
      </v-menu>
    </v-app-bar>
  </nav>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { State, namespace } from 'vuex-class';
import ThemeSwitch from '@/components/globalsettings/ThemeSwitch.vue';
import { User } from '@/models/user';

const authModule = namespace('auth');
const userModule = namespace('user');

@Component({
  components: { ThemeSwitch },
})
export default class Navbar extends Vue {
    drawer: boolean | null = null;

    topSubMenu: { icon: string, text: string, route: string}[] = [
      { icon: 'mdi-account', text: 'views.profile', route: '/profile' },
      { icon: 'mdi-account-cog', text: 'views.settings', route: '/settings' },
      { icon: 'mdi-exit-to-app', text: 'views.signOut', route: '/logout' },
    ];

    links: { icon: string, text: string, route: string}[] = [
      { icon: 'mdi-view-dashboard', text: 'views.dashboard', route: '/' },
      { icon: 'mdi-test-tube', text: 'test.test', route: '/test/12345678?queryParam=test-value' }, // TEST!!!!!
      { icon: 'mdi-console-line', text: 'views.fastEntry', route: '/fastentry' },
      { icon: 'mdi-card-text', text: 'views.entry', route: '/entry' },
    ];

    views: any[] = [
      {
        icon: 'mdi-view-quilt',
        text: 'views.views',
        active: true,
        items: [
          { icon: 'mdi-view-list', text: 'views.overview', route: '/types/all?view=timeline' },
          { icon: 'mdi-note-text', text: 'views.notes', route: '/types/note' },
          { icon: 'mdi-check-box-outline', text: 'views.tasks', route: '/types/task' },
          { icon: 'mdi-bullseye-arrow', text: 'views.goals', route: '/types/goal' },
          { icon: 'mdi-script-text', text: 'views.journal', route: '/types/journal' },
          { icon: 'mdi-run', text: 'views.activities', route: '/types/activity' },
          { icon: 'mdi-flash-circle', text: 'views.habits', route: '/types/habit' },
          { icon: 'mdi-autorenew', text: 'views.routine', route: '/types/routine' },
          { icon: 'mdi-scale-bathroom', text: 'views.weight', route: '/types/weight' },
        ],
      },
      {
        icon: 'mdi-view-quilt',
        text: 'views.filter',
        active: false,
        items: [
          {
            icon: 'mdi-emoticon',
            text: 'views.mood',
            route: '/types/all?view=graph&viewsettings=%7B%22type%22%3A%22Pie%22%2C%22field%22%3A%22mood%22%7D',
          },
          { icon: 'mdi-emoticon', text: 'views.mood', route: '/types/all?view=graph&viewsettings=%7B%22field%22%3A%22mood%22%7D' },
          { icon: 'mdi-run', text: 'views.weight', route: '/types/weight?view=graph&viewsettings=%7B%22field%22%3A%22weight%22%7D' },
          { icon: 'mdi-heart', text: 'views.bucketList', route: '/types/goal?view=card-list&labels=12' },
        ],
      },
    ];

  @authModule.State authenticated!: boolean;

  @userModule.State user!: User | null;
}
</script>
