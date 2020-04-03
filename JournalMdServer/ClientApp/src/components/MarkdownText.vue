<template>
  <span v-html="markdownText" />
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';

const markdownIt = require('markdown-it')({
  linkify: true,
  breaks: true,
});

@Component
export default class MarkdownText extends Vue {
  @Prop({ type: String, required: true }) text!: string;

  get markdownText() {
    return markdownIt.render(this.text);
  }
}
</script>
