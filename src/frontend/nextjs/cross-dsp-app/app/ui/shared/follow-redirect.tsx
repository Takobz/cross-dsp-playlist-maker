'use client'

import { useEffect } from "react";

interface FollowRedirectProps {
    redirectURL: string
}

const FollowRedirect = ({ redirectURL }: FollowRedirectProps) => {
    useEffect(() => {
        window.open(redirectURL, '_blank');
    }, []);

    return <></>
}

export default FollowRedirect;