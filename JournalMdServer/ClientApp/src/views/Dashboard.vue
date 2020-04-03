<template>
  <v-container fill-height>
    <v-row>
      <v-col cols="12">
        <h1>{{ $t('views.dashboard') }}</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="8">
        <v-row>
          <v-col cols="12" md="6">
            <BaseCard class="row1height">TODO Schnellerfassung</BaseCard>
          </v-col>
          <v-col cols="12" md="6">
            <BaseCard class="row1height"><EntryList small /></BaseCard>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="6">
            <TypeViewWrapper viewType="list" :notes="getNotesByType('all')" :maxItems=6 :filterTypes="[1]" compact class="row2height" />
          </v-col>
          <v-col cols="12" md="6">
            <TypeViewWrapper viewType="list" :notes="getNotesByType('all')" :maxItems=6 :filterTypes="[2,3]" compact class="row2height" />
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="6">
            <TypeViewWrapper viewType="graph" :notes="getNotesByType('all')" compact class="row2height" :viewSettings="{'type':'Pie'}" />
          </v-col>
          <v-col cols="12" md="6">
            <Inspiration class="row2height" />
          </v-col>
        </v-row>
      </v-col>
      <v-col cols="12" md="4">
        <v-row>
          <v-col cols="12">
            <TypeViewWrapper viewType="timeline-small" :notes="getNotesByType('all')" :maxItems=13 class="tlheight" />
          </v-col>
        </v-row>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <TypeViewWrapper viewType="card-list" :notes="getNotesByType('all')" :maxItems=6 :filterTypes="[4]" />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <h1>{{ $t('views.dashboard') }} - !!! VIEW TESTS 1 !!!</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="table" :notes="getNotesByType('all')" compact />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="list" :notes="getNotesByType('all')" compact />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="timeline-small" :notes="getNotesByType('all')" compact />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="card-list" :notes="getNotesByType('all')" compact />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="graph" :notes="getNotesByType('all')" compact />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="timeline" :notes="getNotesByType('all')" compact  />
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <h1>{{ $t('views.dashboard') }} - !!! VIEW TESTS 2 !!!</h1>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="table" :notes="getNotesByType('all')" />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="list" :notes="getNotesByType('all')" />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="timeline-small" :notes="getNotesByType('all')" />
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="card-list" :notes="getNotesByType('all')" />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="graph" :notes="getNotesByType('all')" :viewSettings="{'type':'Pie'}" />
      </v-col>
      <v-col cols="12" md="4">
        <TypeViewWrapper viewType="timeline" :notes="getNotesByType('all')"  />
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
  .row1height {
    min-height: 81px;
  }
  .row2height {
    min-height: 440px;
  }
  .tlheight {
    min-height: 1010px;
  }
</style>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins } from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
import NoteTypesMixin from '@/mixins/note-types';
import TypeViewWrapper from '@/components/typeviews/TypeViewWrapper.vue';
import BaseCard from '@/components/BaseCard.vue';
import EntryList from '@/components/EntryList.vue';
import Inspiration from '@/components/Inspiration.vue';

const notesModule = namespace('notes');
const userModule = namespace('user');

@Component({
  components: {
    TypeViewWrapper, BaseCard, EntryList, Inspiration,
  },
})
export default class Dashboard extends Mixins(NoteTypesMixin) {
  @notesModule.State noteTypes: any;

  @notesModule.Getter getNotesByType: any;

  @userModule.Action('getUser') getUserAction: any;

  mounted() {
    this.getUserAction();
  }
}
</script>
