import './page.css'
import { useNavigate } from 'react-router';
import type { DSPPlaylistsResponse } from '../../lib/cross-dsp-api-models';
import { useState } from "react";
import { useDSPFromToSongsContext } from '../../hooks/context-hooks';
import PlaylistCard from '../../ui/playlist/playlist-card';
import { useUserDSPPlaylists } from '../../hooks/cross-dsp-playlists-hooks';

const PlaylistList = () => {
    const router = useNavigate();
    const [playlists, setPlaylists] = useState<DSPPlaylistsResponse | undefined>(undefined);
    const useDSPFromToContext = useDSPFromToSongsContext();

    useUserDSPPlaylists(
        useDSPFromToContext.dspFromToSongs.to,
        (playlists: DSPPlaylistsResponse) => setPlaylists(playlists)
    );

    const onItemsAddedToPlaylist = (result: boolean) => {
        if (result) {
            router("/");
        }
    }
    
    return (
        <div className="playlist-list-container">
            <main className="playlist-list-main">
                {playlists && playlists.data_items ?
                    playlists.data_items.map((playlist, index) => (
                        <PlaylistCard 
                            key={index}
                            playlistId={playlist.playlist_id.id}
                            playlistName={playlist.playlist_name}
                            description={playlist.playlist_discription}
                            onAddItemComplete={onItemsAddedToPlaylist}
                        />
                    )) :
                    <>No Playlists to show</>
                }
            </main>
        </div>
    );
}

export default PlaylistList;