// /** @type {import('tailwindcss').Config} */
const defaultTheme = require("tailwindcss/defaultTheme");

module.exports = {
  content: [
    // Or if using `src` directory:
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        concertOne: ['"Concert One"', ...defaultTheme.fontFamily.sans],
        pressStart2P: ['"Press Start 2P"', ...defaultTheme.fontFamily.sans],
        boogaloo: ['"Boogaloo"', ...defaultTheme.fontFamily.sans],
      },
    },
  },
  plugins: [],
}