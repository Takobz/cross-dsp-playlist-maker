import type { ImageData } from "./definitions";

export type SupportedDSPFromToItem = {
    key: string;
    from: ImageData;
    to: ImageData
}

export const supportedDSPFromToList: SupportedDSPFromToItem[] = [
    {
        key: 'yt-to-spotify',
        from: {
            src: 'youtube-music-icon.svg',
            alt: 'YouTube Music Icon',
            dspName: 'ytmusic',
            dspDisplayName: 'YouTube Music',
            width: 80,
            height: 60
        },
        to: {
            src: 'spotify-green-icon.svg',
            alt: 'Spotify Green Icon',
            dspName: 'spotify',
            dspDisplayName: 'Spotify',
            width: 80,
            height: 60
        }
    }
]; 