export function emoticonicon(value: string | number | null): string {
  // https://materialdesignicons.com/
  switch (value ? value.toString() : '') {
    case '1': return 'mdi-emoticon-dead';
    case '2': return 'mdi-emoticon-sad';
    case '4': return 'mdi-emoticon-happy';
    case '5': return 'mdi-emoticon-excited';
    default: return 'mdi-emoticon-neutral'; // 3 | null
  }
}

export function emoticoncolor(value: string | number | null): string {
  switch (value ? value.toString() : '') {
    case '1': return 'red darken-1';
    case '2': return 'orange darken-1';
    case '4': return 'green darken-';
    case '5': return 'blue darken-1';
    default: return 'brown darken-1'; // 3 | null
  }
}
