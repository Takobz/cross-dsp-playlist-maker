import { type SongDSP } from "../lib/cross-dsp-api-models";
import { type DSPNames } from "../lib/definitions";

export const dspNameFormatter = (dsp: string) : DSPNames => {
    const dspDiplayName = dsp as SongDSP;
    switch(dspDiplayName) {
        case 'YouTube Music':
            return 'ytmusic';
        case 'Spotify':
            return 'spotify'
        default:
            throw new Error(`Unknown DSP: ${dsp} can't be formatted`);
    }
}