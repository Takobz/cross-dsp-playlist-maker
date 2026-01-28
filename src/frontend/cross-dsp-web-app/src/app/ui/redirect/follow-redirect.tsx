
import { type DSPAuthReasons, type DSPNames } from "../../lib/definitions";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useDSPAccessTokenPoller } from "../../hooks/cross-dsp-auth-hooks";

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
    const navigate = useNavigate();

    useEffect(() => {
        window.open(redirectURL, '_blank');
    }, []);

    useDSPAccessTokenPoller(
        dspName,
        authorizationState,
        () => {
            setIsPollingDone(true)
            if (authReason === 'getFromSongs') {
                navigate("/song-search");
            }
            else if (authReason === 'getToSongs'){
                navigate("/playlist")
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