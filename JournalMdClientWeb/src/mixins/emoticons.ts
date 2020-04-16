import Vue from 'vue';
import Component from 'vue-class-component';
import { emoticoncolor, emoticonicon } from '@/helper/emoticonConveters';

@Component({
  filters: {
    emoticonicon: (value: string | number | null) => emoticonicon(value),
    emoticoncolor: (value: string | number | null) => emoticoncolor(value),
  },
})
export default class EmoticonsMixin extends Vue {
}
