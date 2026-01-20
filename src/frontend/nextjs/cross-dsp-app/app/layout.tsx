import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import Header from "./ui/layout/header";
import { DSPAccessTokenContextProvider } from "./context/DSPAccessTokenContextProvider";
import { DSPFromToSongsContextProvider } from "./context/DSPFromToSongsContextProvider";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Cross DSP",
  description: "Generate Playlists across DSPs",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <DSPAccessTokenContextProvider>
      <DSPFromToSongsContextProvider>
        <html lang="en">
          <body
            className={`${geistSans.variable} ${geistMono.variable} antialiased`}
          >
            <Header />
            {children}
          </body>
        </html>
      </DSPFromToSongsContextProvider>
    </DSPAccessTokenContextProvider>
  );
}
