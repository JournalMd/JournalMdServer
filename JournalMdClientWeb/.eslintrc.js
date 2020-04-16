module.exports = {
  root: true,
  env: {
    node: true,
  },
  extends: [
    'plugin:vue/essential',
    '@vue/airbnb',
    '@vue/typescript',
  ],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'max-len': [
      'error',
      {
        code: 140,
      },
    ],
    "linebreak-style": 0,
  },
  parserOptions: {
    parser: '@typescript-eslint/parser',
  },
  settings: {
    'import/resolver': {
      node: {
        paths: ['src/'],
        moduleDirectory: ['node_modules', 'src/'],
        extensions: ['.js', '.jsx', '.ts', '.tsx'],
      }
    }
  },
};
