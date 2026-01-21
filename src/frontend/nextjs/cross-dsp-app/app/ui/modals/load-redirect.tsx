'use client'

import { useEffect, useState } from 'react';
import './load-redirect.css'
import { LoadRedirectProps } from '@/app/lib/definitions';
import FollowRedirect from '../shared/follow-redirect'

const LoadRedirect = ({ 
        getRedirectFunction, 
        dspName,
        authReason
    }: LoadRedirectProps) => {
    const [redirect, setRedirect] = useState<string>("");
    const [authorizationState, setAuthorizationState] = useState<string>("");

    useEffect(() => {
        async function getRedirect() {
            const response = await getRedirectFunction();
            setRedirect(response.data.authorization_code_flow_redirect);
            setAuthorizationState(response.data.authorization_state);
        }

        getRedirect();
    }, []);

    return (
    <>
        {redirect ? 
            <FollowRedirect 
                dspName={dspName} 
                authReason={authReason}
                redirectURL={redirect} 
                authorizationState={authorizationState}
            /> : <></>
        }
    </>
    );
}

export default LoadRedirect;