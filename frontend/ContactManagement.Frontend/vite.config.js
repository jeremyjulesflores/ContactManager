import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [react()],
  server: {
    watch:{
      usePolling:true
    },
    host: true,
    strictPort:true,
    port: 5173
  }
}
