import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import macrosPlugin from 'vite-plugin-babel-macros'
import fs from 'fs'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), macrosPlugin()],
  server: {
    https: {
      key: fs.readFileSync('dotnet-dev-cert.key'),
      cert: fs.readFileSync('dotnet-dev-cert.crt')
    }
  }
})
