'use client'

import { usePathname, useSearchParams } from "next/navigation";
import { useEffect } from "react";
import LoadRedirect from "../ui/modals/load-redirect";
import { useCrossDSPAuthorization } from "../hooks/cross-dsp-api-hooks";
import { DSPNames } from "../lib/definitions";

const AuthorizePage = () => {
    const searchParams = useSearchParams();
    const from = searchParams.get('from');
    const to = searchParams.get('to');

    const dspAuthFunction = useCrossDSPAuthorization(from as DSPNames);

    return (
        <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
            <main className="flex min-h-screen w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start">
                {from ? <LoadRedirect getRedirectFunction={dspAuthFunction}/> : <p>Presented From DSP Unknown</p>}
            </main>
        </div>
    );
}

export default AuthorizePage;