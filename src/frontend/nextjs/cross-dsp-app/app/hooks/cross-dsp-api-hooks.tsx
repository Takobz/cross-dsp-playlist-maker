import { useContext, useEffect } from "react";
import { DSPAccessTokenResponse } from "../lib/cross-dsp-api-models";
import { getGoogleAccessToken, getGoogleRedirect } from "../lib/cross-dsp-api-service"
import { DSPNames } from "../lib/definitions"
import { DSPAccessTokensContext } from "@/app/context/DSPAccessTokenContextProvider"

const useCrossDSPAuthorization = (dspName: DSPNames) => {
    switch (dspName) {
        case DSPNames.ytmusic:
            return getGoogleRedirect;
        default:
            throw new Error(`Provided DSP: ${dspName} has no authorization handler`);
    }
}

let INTERVAL_ID: NodeJS.Timeout;
const useDSPAccessTokenPoller = (
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
    switch (dspName) {
        case DSPNames.ytmusic:
            await setDSPAccessToken(
                dspName, 
                authorizationState,
                dspAccessTokenContext!,
                onPollComplete
            );
            break;

        default:
            throw new Error(`Provided DSP: ${dspName} not supported.`);
    }
};

async function setDSPAccessToken(
    dspName: DSPNames, 
    authorizationState: string,
    dspAccessTokensContext: DSPAccessTokensContext,
    onPollComplete: () => void
) {
    const currentTokens = dspAccessTokensContext.dspTokens;
    const token = await getGoogleAccessToken(authorizationState);

    if (token.data) {
        clearInterval(INTERVAL_ID);

        switch(dspName){
            case DSPNames.ytmusic:
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

            default:
                throw new Error(`Provided DSP: ${dspName} not supported.`);
        }
    }

    if (token.error_messages.length > 0) {
        //will need to prop this to the user some how.
        console.log(token.error_messages)
    }
}

export { 
    useCrossDSPAuthorization,
    useDSPAccessTokenPoller
}