export function typeicon(value: string | number | null): string {
  if (value === null || !value) return '';

  // https://materialdesignicons.com/
  // TODO better way for ids...
  switch (value.toString()) {
    case 'all': case '0': return 'mdi-view-list'; // special case
    case 'task': case '2': return 'mdi-check-box-outline';
    case 'goal': case '3': return 'mdi-bullseye-arrow';
    case 'journal': case '4': return 'mdi-script-text';
    case 'activity': case '5': return 'mdi-run';
    case 'habit': case '6': return 'mdi-flash-circle';
    case 'routine': case '7': return 'mdi-autorenew';
    case 'weight': case '8': return 'mdi-scale-bathroom';
    default: return 'mdi-note-text';
  }
}

export function typecolor(value: string | number | null): string {
  if (value === null || !value) return '';

  // TODO better way for ids...
  switch (value.toString()) {
    // case 'all': case '0': return ' darken-1'; // special case
    case 'task': case '2': return 'red darken-1';
    case 'goal': case '3': return 'blue darken-1';
    case 'journal': case '4': return 'cyan darken-1';
    case 'activity': case '5': return 'orange darken-1';
    case 'habit': case '6': return 'deep-orange darken-1';
    case 'routine': case '7': return 'brown darken-1';
    case 'weight': case '8': return 'purple darken-1';
    default: return 'green darken-1';
  }
}
