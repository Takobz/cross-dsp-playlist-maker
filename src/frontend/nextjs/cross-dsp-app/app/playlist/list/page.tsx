'use client'

import { useDSPFromToSongsContext } from "@/app/hooks/context-hooks";
import { useUserDSPPlaylists } from "@/app/hooks/cross-dsp-playlists-hooks";
import { DSPPlaylistsResponse } from "@/app/lib/cross-dsp-api-models";
import PlaylistCard from "@/app/ui/playlist/playlist-card";
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
            <main className="flex flex-col align-center items-center justify-center mt-20">
                {playlists && playlists.data_items ?
                    playlists.data_items.map((playlist, index) => (
                        <PlaylistCard 
                            key={index}
                            playlistId={playlist.playlist_id.id}
                            playlistName={playlist.playlist_name}
                            description={playlist.playlist_discription}
                        />
                    )) :
                    <>No Playlists to show</>
                }
            </main>
        </div>
    );
}

export default PlaylistList;