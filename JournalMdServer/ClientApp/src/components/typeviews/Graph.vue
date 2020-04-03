<template>
  <chartist
      class="ct-chart chart"
      :ratio="compact ? 'ct-perfect-fourth' : 'ct-minor-seventh'"
      :type="chartType"
      :data="chartData"
      :options="chartOptions"
    >
  </chartist>
</template>

<style lang="scss">
  @import "chartist/dist/scss/chartist.scss";

  .ct-chart-pie > g > .ct-label {
    font-size: 32px;
  }
  .ct-series-a .ct-slice-pie { fill: #E53935; }
  .ct-series-b .ct-slice-pie { fill: #FB8C00; }
  .ct-series-c .ct-slice-pie { fill: #6D4C41; }
  .ct-series-d .ct-slice-pie { fill: #43A047; }
  .ct-series-e .ct-slice-pie { fill: #1E88E5; }
</style>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Watch,
} from 'vue-property-decorator';
import moment from 'moment';
import _ from 'lodash';
import NoteTypesMixin from '@/mixins/note-types';

Vue.use(require('./vue-chartist.js'), { messageNoData: 'You have not enough data', classNoData: 'empty' });

const ChartistLib = require('chartist');

@Component
export default class Graph extends Mixins(NoteTypesMixin) {
  @Prop(Array) notes!: any[];

  @Prop(Object) viewSettings!: any;

  @Prop(Boolean) compact!: boolean;

  data: { labels: string[], series: number[] | [number[]] } = {
    labels: [],
    series: [[]],
  }

  chartType: string = 'Line';

  field: string = 'mood';

  mounted() {
    this.updateData();
  }

  @Watch('viewSettings')
  onSettingsChange() {
    this.updateData();
  }

  updateData() {
    this.field = this.viewSettings && this.viewSettings.field ? this.viewSettings.field : 'mood';

    if (this.field === 'mood') {
      this.chartType = this.viewSettings && this.viewSettings.type ? this.viewSettings.type : 'Bar'; // Mood looks better with Bar
    } else {
      this.chartType = this.viewSettings && this.viewSettings.type ? this.viewSettings.type : 'Line'; // Fields get line/timeSeries
    }
    console.log(this.chartType);

    let labels: string[] = [];
    let series: any = [];

    if (this.field === 'mood') {
      labels = ['😣', '🙁', '😐', '🙂', '😄'];
      series = [ // quick and dirty mood count ;)
        this.notes.filter(sel => sel.mood === 1).length,
        this.notes.filter(sel => sel.mood === 2).length,
        this.notes.filter(sel => sel.mood === 3 || sel.mood == null).length,
        this.notes.filter(sel => sel.mood === 4).length,
        this.notes.filter(sel => sel.mood === 5).length,
      ];
    } else {
      series = {
        name: 'field-series',
        // data [{ x: new Date(143134652600), y: 53 }, ...]
        data: _(this.notes.map(sel => ({
          x: sel.createdAt,
          y: +sel.fields[this.field].value,
        }))).orderBy('x', 'asc').value(),
      };
    }

    // Pie [], Line [[]]
    if (this.chartType === 'Pie') {
      this.data = {
        labels,
        series,
      };
    } else {
      this.data = {
        labels,
        series: [series],
      };
    }
    console.log(this.data);
  }

  get chartOptions() {
    if (this.chartType === 'Pie') {
      return {};
    }

    if (this.chartType === 'Bar') {
      return {
        axisY: {
          onlyInteger: true,
        },
      };
    }

    return {
      axisY: {
        onlyInteger: true,
      },
      axisX: {
        type: ChartistLib.FixedScaleAxis,
        divisor: 5,
        labelInterpolationFnc(value: any) {
          return moment(value).format('MMM D');
        },
      },
    };
  }

  // eslint-disable-next-line class-methods-use-this
  get chartData() {
    return {
      labels: this.data.labels,
      series: this.data.series,
    };
  }
}
</script>
