'use client'

import { useDSPAccessTokenPoller } from "@/app/hooks/cross-dsp-api-hooks";
import { DSPNames } from "@/app/lib/definitions";
import { useEffect } from "react";

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
    useEffect(() => {
        window.open(redirectURL, '_blank');
    }, []);

    useDSPAccessTokenPoller(
        dspName,
        authorizationState
    );

    return <>Waiting for Access Token</>
}

export default FollowRedirect;