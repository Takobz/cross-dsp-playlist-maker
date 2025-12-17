import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  async rewrites() {
    return [
      {
        source: '/dsp-api/:path*',
        destination: 'http://localhost:5080/:path*'
      }
    ]
  },
  reactStrictMode: false
};

export default nextConfig;
