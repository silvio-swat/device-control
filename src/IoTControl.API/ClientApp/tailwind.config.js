/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
    "./node_modules/primeng/**/*.{js,ts,html}" // Permite que o Tailwind estilize componentes PrimeNG
  ],
  theme: {
    extend: {},
  }
}
