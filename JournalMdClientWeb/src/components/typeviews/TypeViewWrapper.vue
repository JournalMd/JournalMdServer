<template>
  <BaseCard>
    <Table v-if="viewType === 'table'" v-bind="$props" :notes="filteredNotes" :noteTypes="noteTypes" :labels="labels"
       @edit-note="onEdit" @delete-note="onDelete" />
    <Timeline v-else-if="viewType === 'timeline'" v-bind="$props" :notes="filteredNotes" :noteTypes="noteTypes" :labels="labels"
       @edit-note="onEdit" @delete-note="onDelete" />
    <TimelineSmall v-else-if="viewType === 'timeline-small'" v-bind="$props" :notes="filteredNotes" :noteTypes="noteTypes"
      :labels="labels" @edit="onEdit" @delete-note="onDelete" />
    <List v-else-if="viewType === 'list'" v-bind="$props" :notes="filteredNotes" :noteTypes="noteTypes" :labels="labels"
       @edit-note="onEdit" @delete-note="onDelete" />
    <CardList v-else-if="viewType === 'card-list'" v-bind="$props" :notes="filteredNotes" :noteTypes="noteTypes" :labels="labels"
       @edit-note="onEdit" @delete-note="onDelete" />
    <Graph v-else-if="viewType === 'graph'" v-bind="$props" :notes="filteredNotes" :noteTypes="noteTypes" :labels="labels"
       @edit-note="onEdit" @delete-note="onDelete" />
    <div v-else>-</div>
  </BaseCard>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Mixins, Prop } from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
// import orderBy from 'lodash/orderBy';
import _ from 'lodash';
import NoteTypesMixin from '@/mixins/note-types';
import BaseCard from '@/components/BaseCard.vue';
import Table from './Table.vue';
import Timeline from './Timeline.vue';
import TimelineSmall from './TimelineSmall.vue';
import List from './List.vue';
import CardList from './CardList.vue';
import Graph from './Graph.vue';

const notesModule = namespace('notes');
const dialogsModule = namespace('dialogs');

// Template options:
// notes, compact

@Component({
  components: {
    BaseCard,
    Table,
    Timeline,
    TimelineSmall,
    List,
    CardList,
    Graph,
  },
})
export default class TypeViewWrapper extends Mixins(NoteTypesMixin) {
  @Prop(String) title!: string;

  @Prop(String) viewType!: string;

  @Prop(Object) viewSettings!: {};

  @Prop(Array) notes!: any[];

  @Prop(Boolean) compact!: boolean;

  @Prop(Number) maxItems!: number;

  @Prop(Array) filterTypes!: number[];

  @Prop(Array) filterLabels!: number[];

  @notesModule.State noteTypes: any;

  @notesModule.State labels: any;

  @notesModule.Getter getNotesByType: any;

  get filteredNotes() {
    // return orderBy(this.notes, 'createdAt', 'desc');
    let filter = _(this.notes);

    if (this.filterTypes != null && this.filterTypes.length > 0) {
      // find typeId in filterTypes array
      filter = filter.filter(sel => this.filterTypes.indexOf(sel.typeId) !== -1);
    }

    if (this.filterLabels != null && this.filterLabels.length > 0) {
      // find one matching label id from labels array in filterLabels array
      filter = filter.filter(sel => sel.labels
        .some((labelId: number) => this.filterLabels
          .find(subSel => subSel === labelId) !== undefined));
    }

    filter = filter.orderBy(['typeId', 'createdAt'], ['asc', 'desc']); // order before take!

    if (this.maxItems != null) {
      filter = filter.take(this.maxItems);
    }

    return filter.value();
  }

  @dialogsModule.Action('editNote') editNoteAction!: any;

  onEdit(note: any) {
    this.editNoteAction(note);
  }

  @dialogsModule.Action('deleteNote') deleteNoteAction!: any;

  onDelete(note: any) {
    this.deleteNoteAction(note);
  }
}
</script>
