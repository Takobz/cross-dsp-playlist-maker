import { useContext } from "react";
import { DSPFromToSongsContext } from "../../../../cross-dsp-web-app/src/app/context/DSPFromToSongsContextProvider";
import { DSPAccessTokensContext } from "../context/DSPAccessTokenContextProvider";
import { DSPUsersContext } from "../context/DSPUsersContextProvider";

export const useDSPFromToSongsContext = () => {
    const dspFromToSongsContext = useContext(DSPFromToSongsContext);
    if (dspFromToSongsContext === undefined || dspFromToSongsContext === null){
        throw Error(
            `${typeof(DSPFromToSongsContext)} should be used in components wrapped by the context provider`
        );
    }

    return dspFromToSongsContext;
}

export const useDSPAccessTokensContext = () => {
    const dspAccessTokensContext = useContext(DSPAccessTokensContext);
    if (dspAccessTokensContext === undefined){
        throw Error(
            `${typeof(DSPAccessTokensContext)} should be used in components wrapped by the context provider`
        );
    }

    return dspAccessTokensContext;
}

export const useDSPUsersContext = () => {
    const dspUsersContext = useContext(DSPUsersContext);
    if (dspUsersContext === undefined){
        throw Error(
            `${typeof(dspUsersContext)} should be used in components wrapped by the context provider`
        );
    }

    return dspUsersContext;
}