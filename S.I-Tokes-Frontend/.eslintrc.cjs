import '@rushstack/eslint-patch'

module.exports = {
    root: true,
    'extendes': [
        'plugin:vue/vue3-essential',
        'eslint:recommended',
        '@vue/eslint-config-prettier/skip-formatting'
    ],
    parserOptions: {
        ecmaVersion: 'latest'
    }
}