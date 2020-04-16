import Vue from 'vue';
import Component from 'vue-class-component';
import { typecolor, typeicon } from '@/helper/typeConverter';

@Component({
  filters: {
    typeicon: (value: string | number | null) => typeicon(value),
    typecolor: (value: string | number | null) => typecolor(value),
  },
})
export default class NoteTypesMixin extends Vue {
}
