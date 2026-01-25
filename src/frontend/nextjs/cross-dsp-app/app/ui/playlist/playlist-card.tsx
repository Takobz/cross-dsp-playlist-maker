'use client'

import { useUserPlaylistAdd } from '@/app/hooks/cross-dsp-playlists-hooks'
import './playlist-card.css'
import { useDSPFromToSongsContext } from '@/app/hooks/context-hooks'
import { useState } from 'react'

type PlaylistCardProps = {
    playlistId: string
    playlistName: string,
    description: string
}

const PlaylistCard = ({
    playlistId,
    playlistName,
    description: descripion
}: PlaylistCardProps) => {
    const [isSuccess, setIsSuccess] = useState<boolean>(false);
    const dspFromToSongsContext = useDSPFromToSongsContext();

    const onComplete = (result: boolean) => {
        setIsSuccess(result)
    }

    const onAddToPlaylist = () => {
        useUserPlaylistAdd(
            dspFromToSongsContext.dspFromToSongs.to,
            playlistId,
            dspFromToSongsContext.dspFromToSongs.toSongs,
            onComplete
        );
    }

    return (
        <>
            {isSuccess ?
                <div className="playlist-card-container">
                    <h1>
                        <strong>{playlistName}</strong>
                    </h1>
                    <p className="playlist-card-description ">{descripion}</p>

                    <div className="playlist-add-button" onClick={onAddToPlaylist}>
                        Add to Playlist
                    </div>
                </div> : <>Done Boy Bye!</>
            }
        </>

    );
}

export default PlaylistCard;