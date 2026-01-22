'use client'

import { useDSPAccessTokenPoller } from "@/app/hooks/cross-dsp-api-hooks";
import { DSPAuthReasons, DSPNames } from "@/app/lib/definitions";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

interface FollowRedirectProps {
    redirectURL: string,
    dspName: DSPNames,
    authorizationState: string
    authReason: DSPAuthReasons
}

const FollowRedirect = ({
    redirectURL,
    dspName,
    authorizationState,
    authReason
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
            if (authReason === DSPAuthReasons.getFromSongs) {
                navigate.push("song-search");
            }
            else if (authReason === DSPAuthReasons.getToSongs){
                navigate.push("playlist")
            }
        }
    );

    return (
        <>
            {isPollingDone ? <>Taking you to the search</> :
             <>Waiting for Access Token</>
            }
        </>
    );
}

export default FollowRedirect;