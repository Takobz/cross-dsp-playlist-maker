import { SongDSP } from "@/app/lib/cross-dsp-api-models";
import { DSPNames } from "@/app/lib/definitions";

const dspNameFormatter = (dsp: string) => {
    const dspDiplayName = dsp as SongDSP;
    switch(dspDiplayName) {
        case SongDSP.ytmusic:
            return DSPNames.ytmusic;
        case SongDSP.spotify:
            return DSPNames.spotify
        default:
            throw new Error(`Unknown DSP: ${dsp} can't be formatted`);
    }
}

export {
    dspNameFormatter
}