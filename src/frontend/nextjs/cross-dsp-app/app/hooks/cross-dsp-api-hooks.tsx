import { getGoogleRedirect } from "../lib/cross-dsp-api-service"
import { DSPNames } from "../lib/definitions"

const useCrossDSPAuthorization = (dspName: DSPNames) => {
    switch (dspName) {
        case DSPNames.ytmusic:
            return getGoogleRedirect;
        default:
            throw new Error(`Provided DSP: ${dspName} has no authorization handler`);
    }
}

export { 
    useCrossDSPAuthorization 
}