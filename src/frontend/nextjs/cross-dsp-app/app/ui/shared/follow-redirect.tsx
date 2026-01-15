'use client'

import { DSPAccessTokensContext } from "@/app/context/DSPAccessTokenContextProvider";
import { useDSPAccessTokenPoller } from "@/app/hooks/cross-dsp-api-hooks";
import { DSPNames } from "@/app/lib/definitions";
import { useContext, useEffect, useState } from "react";

interface FollowRedirectProps {
    redirectURL: string,
    dspName: DSPNames,
    authorizationState: string
}

const FollowRedirect = ({
    redirectURL,
    dspName,
    authorizationState
}: FollowRedirectProps) => {
    const [isPollingDone, setIsPollingDone] = useState(false);
    const dspAccessTokensContext = useContext(DSPAccessTokensContext);

    useEffect(() => {
        window.open(redirectURL, '_blank');
    }, []);

    useDSPAccessTokenPoller(
        dspName,
        authorizationState,
        () => setIsPollingDone(true)
    );

    //find a way to know we are done polling...

    return (
        <>
            {isPollingDone ? <>{JSON.stringify(dspAccessTokensContext?.dspTokens)}</> :
             <>Waiting for Access Token</>
            }
        </>
    );
}

export default FollowRedirect;