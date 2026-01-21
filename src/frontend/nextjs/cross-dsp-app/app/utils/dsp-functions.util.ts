// Util for getting different functions based on dsp name provided.

import { DSPSongsResponse } from "../lib/cross-dsp-api-models";
import { getSpotifySongsByArtistAndName } from "../lib/cross-dsp-api-service";
import { DSPNames } from "../lib/definitions";

export const getToSongsFunctions = (dspName: DSPNames) 
    : ((songName: string, artistName: string) => Promise<DSPSongsResponse>) => {
        switch (dspName) {
            case DSPNames.spotify:
                return getSpotifySongsByArtistAndName;
            default:
                throw Error(`DSP: ${dspName} has no to functions`);
        }
}