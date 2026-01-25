import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  async rewrites() {
    return [
      {
        source: '/:path*/dsp-api/:rest*',
        destination: 'http://localhost:5080/:rest*'
      }
    ]
  },
  reactStrictMode: false
};

export default nextConfig;
