import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '^/.*dsp-api/': {
        target: 'http://localhost:5080',
        changeOrigin: true,
        rewrite: (path) => {
          const match = path.match(/\/dsp-api\/(.*)$/);
          return match ? `/${match[1]}` : path;
        }
      }
    }
  }
})

/**
 * Below will be proxying for built app in nginx:
 * location ~* /dsp-api/ {
    rewrite ^/(.*)dsp-api/(.*)$ /$2 break;
    proxy_pass http://localhost:5080;
  }
 */
