'use client'

import { useEffect, useState } from 'react';
import './load-redirect.css'
import { LoadRedirectProps } from '@/app/lib/definitions';
import FollowRedirect from '../shared/follow-redirect'

const LoadRedirect = ({ getRedirectFunction }: LoadRedirectProps) => {
    const [redirect, setRedirect] = useState<string>("");

    useEffect(() => {
        async function getRedirect() {
            const response = await getRedirectFunction();
            setRedirect(response.data.authorization_code_flow_redirect);
        }

        getRedirect();
    }, []);

    return (redirect ? <FollowRedirect redirectURL={redirect} /> : <></>);
}

export default LoadRedirect;