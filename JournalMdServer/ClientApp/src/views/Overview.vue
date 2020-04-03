<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <h1>
          <v-icon size="32" style="vertical-align: baseline">{{ route.params.type | typeicon }}</v-icon> {{ $t('views.overview') }}
        </h1>
      </v-col>
      <v-col cols="12">
        <BaseCard title="Filter">
          <v-row>
            <v-col cols="6">
              <v-select :items="viewTypes" v-model="selectedViewType" dense label="View type"></v-select>
            </v-col>
            <v-col cols="6">
              <v-select :items="noteTypes" item-value="name" item-text="title" v-model="type" dense
                label="Node type" disabled></v-select>
            </v-col>
          </v-row>
        </BaseCard>
      </v-col>

      <v-col cols="12">
        <TypeViewWrapper
          :viewType="selectedViewType"
          :notes="getNotesByType(route.params.type)"
          :filterLabels="filterLabels"
          :viewSettings="viewSettings"
        />
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
import NoteTypesMixin from '@/mixins/note-types';
import TypeViewWrapper from '@/components/typeviews/TypeViewWrapper.vue';
import BaseCard from '@/components/BaseCard.vue';

const notesModule = namespace('notes');

@Component({
  components: { BaseCard, TypeViewWrapper },
})
export default class Overview extends Mixins(NoteTypesMixin) {
  @Prop(String) type!: string;

  @State route: any; // vuex-router-sync

  currentNodeType: string = '';

  selectedViewType: string = '';

  viewSettings: any = {};

  filterLabels: number[] = [];

  viewTypes: { value: string, text: string }[] = [
    { value: 'table', text: 'Table' },
    { value: 'timeline', text: 'Timeline' },
    { value: 'timeline-small', text: 'Timeline small' },
    { value: 'list', text: 'List' },
    { value: 'card-list', text: 'Card list' },
    { value: 'graph', text: 'Graph' },
  ];

  @notesModule.State noteTypes: any;

  @notesModule.Getter getNotesByType: any;

  mounted() {
    this.selectedViewType = this.route.query.view ? this.route.query.view : 'table';

    this.viewSettings = this.route.query.viewsettings ? JSON.parse(this.route.query.viewsettings) : {};

    this.filterLabels = this.route.query.labels ? this.route.query.labels.split(',').map((sel: string) => +sel) : [];
  }

  @Watch('$route')
  onRouteChanged(to: any, from: any) {
    if (to.query.view) {
      this.selectedViewType = to.query.view;
    }

    this.viewSettings = to.query.viewsettings ? JSON.parse(to.query.viewsettings) : {};

    this.filterLabels = to.query.labels ? to.query.labels.split(',').map((sel: string) => +sel) : [];
  }
}
</script>
