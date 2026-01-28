import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import Header from "./ui/layout/header";
import { DSPAccessTokenContextProvider } from "./context/DSPAccessTokenContextProvider";
import { DSPFromToSongsContextProvider } from "../../../cross-dsp-web-app/src/app/context/DSPFromToSongsContextProvider";
import { DSPUsersContextProvider } from "./context/DSPUsersContextProvider";

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
        <DSPUsersContextProvider>
          <html lang="en">
            <body
              className={`${geistSans.variable} ${geistMono.variable} antialiased`}
            >
              <Header />
              {children}
            </body>
          </html>
        </DSPUsersContextProvider>
      </DSPFromToSongsContextProvider>
    </DSPAccessTokenContextProvider>
  );
}
