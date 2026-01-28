import { useContext, useEffect } from "react";
import type { DSPNames } from "../lib/definitions";
import { DSPAccessTokensContext } from "../context/DSPAccessTokenContextProvider";
import { getAccessTokenFunction, getDSPRedirectFunction } from "../utils/dsp-functions.util";


export const useCrossDSPAuthorization = (dspName: DSPNames) => {
    return getDSPRedirectFunction(dspName);
}

let INTERVAL_ID: number;
export const useDSPAccessTokenPoller = (
    dspName: DSPNames, 
    authorizationState: string,
    onPollingComplete: () => void
) => {
    const dspAccessTokensContext = useContext(DSPAccessTokensContext);
    if (dspAccessTokensContext === undefined){
        throw Error(
            `${typeof(DSPAccessTokensContext)} should be used in components wrapped by the context provider`
        );
    }

    useEffect(() => {
        const delayInMilliSeconds = 10000;
        INTERVAL_ID = setInterval(
            accessTokenPoller,
            delayInMilliSeconds,  
            dspName, 
            authorizationState,
            dspAccessTokensContext,
            onPollingComplete
        );
    }, [dspName, authorizationState]);
}

const accessTokenPoller = async (
    dspName: DSPNames, 
    authorizationState: string,
    dspAccessTokenContext: DSPAccessTokensContext,
    onPollComplete: () => void
) => {      

    await setDSPAccessToken(
        dspName, 
        authorizationState,
        dspAccessTokenContext!,
        onPollComplete
    );
};

async function setDSPAccessToken(
    dspName: DSPNames, 
    authorizationState: string,
    dspAccessTokensContext: DSPAccessTokensContext,
    onPollComplete: () => void
) {
    const currentTokens = dspAccessTokensContext.dspTokens;
    const accessTokenFunc = getAccessTokenFunction(dspName);
    const token = await accessTokenFunc(authorizationState);

    if (token.data) {
        clearInterval(INTERVAL_ID);

        switch(dspName){
            case 'ytmusic':
                dspAccessTokensContext.setDSPTokens(
                {
                    ...currentTokens, 
                    Google: {
                        AccessToken: token.data.access_token,
                        RefreshToken: token.data.refresh_token,
                        ExpiresIn: token.data.expires_in
                    }
                });
                onPollComplete();
                break;

            case 'spotify':
                dspAccessTokensContext.setDSPTokens({
                    ...currentTokens,
                    Spotify: {
                        AccessToken: token.data.access_token,
                        RefreshToken: token.data.refresh_token,
                        ExpiresIn: token.data.expires_in
                    }
                })
                onPollComplete();
                break;

            default:
                throw new Error(`Provided DSP: ${dspName} not supported.`);
        }
    }

    if (token.error_messages.length > 0) {
        //will need to prop this to the user some how.
        console.log(token.error_messages)
    }
}