'use client'

import { useSearchParams } from "next/navigation";
import LoadRedirect from "../ui/modals/load-redirect";
import { useCrossDSPAuthorization } from "../hooks/cross-dsp-api-hooks";
import { DSPAuthReasons, DSPNames } from "../lib/definitions";

const AuthorizePage = () => {
    const searchParams = useSearchParams();
    const dsp = searchParams.get('dsp') as DSPNames;
    const reason = searchParams.get('reason') as DSPAuthReasons 

    const dspAuthFunction = useCrossDSPAuthorization(dsp);

    return (
        <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
            <main className="flex min-h-screen w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start">
                {dsp != null ? 
                    <LoadRedirect 
                        dspName={dsp} 
                        authReason={reason}
                        getRedirectFunction={dspAuthFunction}
                    /> : 
                    <p>Presented From DSP Unknown</p>
                }
            </main>
        </div>
    );
}

export default AuthorizePage;