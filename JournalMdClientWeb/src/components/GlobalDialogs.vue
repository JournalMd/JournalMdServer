<template>
  <div>
    <v-snackbar v-model="messageSnackbar" :timeout="6000" bottom left color="success">
      <span>{{ message }}</span>
      <v-btn text @click="snackbarMessages = false; clearMessages()">{{ $t('general.close') }}</v-btn>
    </v-snackbar>

    <v-snackbar v-model="errorSnackbar" :timeout="6000" bottom left color="error">
      <span>{{ error }}</span>
      <v-btn text @click="snackbarErrors = false; clearErrors()">{{ $t('general.close') }}</v-btn>
    </v-snackbar>

    <CreateEditDialog />
    <DeleteDialog />
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import {
  Component, Mixins, Prop, Watch,
} from 'vue-property-decorator';
import { State, Getter, namespace } from 'vuex-class';
import CreateEditDialog from '@/components/notedialogs/CreateEditDialog.vue';
import DeleteDialog from '@/components/notedialogs/DeleteDialog.vue';

const dialogsModule = namespace('dialogs');

@Component({
  components: { CreateEditDialog, DeleteDialog },
})
export default class GlobalDialogs extends Vue {
  errorSnackbar: boolean = false;

  messageSnackbar: boolean = false;

  get error() {
    this.errorSnackbar = this.$store.state.dialogs.error.length > 0;
    return this.$store.state.dialogs.error;
  }

  get message() {
    this.messageSnackbar = this.$store.state.dialogs.message.length > 0;
    return this.$store.state.dialogs.message;
  }

  @dialogsModule.Action('clearError') clearErrorAction: any;

  @dialogsModule.Action('clearMessage') clearMessageAction: any;

  clearErrors() {
    this.errorSnackbar = false;
    this.clearErrorAction();
  }

  clearMessages() {
    this.messageSnackbar = false;
    this.clearMessageAction();
  }

  @Watch('errorSnackbar')
  onErrorDialogChange(newValue: boolean, oldValue: boolean) {
    if (this.$store.state.dialogs.error.length > 0 && newValue === false) {
      this.clearErrors(); // reset "current" selection to null to allow reopening the dialog
    }
  }

  @Watch('messageSnackbar')
  onMessageDialogChange(newValue: boolean, oldValue: boolean) {
    if (this.$store.state.dialogs.message.length > 0 && newValue === false) {
      this.clearMessages(); // reset "current" selection to null to allow reopening the dialog
    }
  }
}
</script>
