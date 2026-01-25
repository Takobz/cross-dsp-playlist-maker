'use client'

import { useDSPFromToSongsContext } from "@/app/hooks/context-hooks";
import { useUserDSPPlaylists } from "@/app/hooks/cross-dsp-playlists-hooks";
import { DSPPlaylistsResponse } from "@/app/lib/cross-dsp-api-models";
import { useState } from "react";

const PlaylistList = () => {
    const [playlists, setPlaylists] = useState<DSPPlaylistsResponse | undefined>(undefined);
    const useDSPFromToContext = useDSPFromToSongsContext();

    useUserDSPPlaylists(
        useDSPFromToContext.dspFromToSongs.to,
        (playlists: DSPPlaylistsResponse) => setPlaylists(playlists)
    );
    
    return (
        <div className="flex min-h-screen items-center justify-center">
            <main className="flex flex-col">
                <>{JSON.stringify(playlists)}</>
            </main>
        </div>
    );
}

export default PlaylistList;