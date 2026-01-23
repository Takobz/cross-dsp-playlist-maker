import { useContext } from "react";
import { DSPFromToSongsContext } from "../context/DSPFromToSongsContextProvider";
import { DSPAccessTokensContext } from "../context/DSPAccessTokenContextProvider";

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