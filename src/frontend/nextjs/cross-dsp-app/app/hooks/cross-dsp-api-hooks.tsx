import { DSPAccessTokenResponse } from "../lib/cross-dsp-api-models";
import { getGoogleAccessToken, getGoogleRedirect } from "../lib/cross-dsp-api-service"
import { DSPNames } from "../lib/definitions"

const useCrossDSPAuthorization = (dspName: DSPNames) => {
    switch (dspName) {
        case DSPNames.ytmusic:
            return getGoogleRedirect;
        default:
            throw new Error(`Provided DSP: ${dspName} has no authorization handler`);
    }
}

let INTERVAL_ID: NodeJS.Timeout;
const useDSPAccessTokenPoller = async (
    dspName: DSPNames, 
    authorizationState: string
) => {
    const delayInMilliSeconds = 10000;

    INTERVAL_ID = setInterval(
        accessTokenPoller,
        delayInMilliSeconds,  
        dspName, 
        authorizationState
    );
    
}

const accessTokenPoller = async (
    dspName: DSPNames, 
    authorizationState: string
) => {
        let tokenResponse = {} as DSPAccessTokenResponse;
        
        switch (dspName) {
            case DSPNames.ytmusic:
                tokenResponse = await getGoogleAccessToken(authorizationState);
                break;
            default:
                throw new Error(`Provided DSP: ${dspName} not supported.`);
        }
        
        if (tokenResponse.data && tokenResponse.data){
            clearInterval(INTERVAL_ID);
        }

        if (tokenResponse.error_messages.length > 0) {
            //will need to prop this to the user some how.
            console.log(tokenResponse.error_messages)
        }
    };

export { 
    useCrossDSPAuthorization,
    useDSPAccessTokenPoller
}