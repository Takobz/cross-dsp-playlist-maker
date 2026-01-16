'use client'

import { useDSPAccessTokenPoller } from "@/app/hooks/cross-dsp-api-hooks";
import { DSPNames } from "@/app/lib/definitions";
import { useRouter } from "next/navigation";
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
    const navigate = useRouter();

    useEffect(() => {
        window.open(redirectURL, '_blank');
    }, []);

    useDSPAccessTokenPoller(
        dspName,
        authorizationState,
        () => {
            setIsPollingDone(true)
            navigate.push("song-search");
        }
    );

    //find a way to know we are done polling...

    return (
        <>
            {isPollingDone ? <>Taking you to the search</> :
             <>Waiting for Access Token</>
            }
        </>
    );
}

export default FollowRedirect;