import { DSPDisplayNames, DSPNames } from "./definitions";

const supportedDSPFromToList = [
    {
        key: 'yt-to-spotify',
        from: {
            src: '/youtube-music-icon.svg',
            alt: 'YouTube Music Icon',
            dspName: DSPNames.ytmusic,
            dspDisplayName: DSPDisplayNames.ytmusic,
            width: 80,
            height: 20
        },
        to: {
            src: '/spotify-green-icon.svg',
            alt: 'Spotify Green Icon',
            dspName: DSPNames.spotify,
            dspDisplayName: DSPDisplayNames.spotify,
            width: 80,
            height: 20
        }
    }
]; 

export { supportedDSPFromToList }