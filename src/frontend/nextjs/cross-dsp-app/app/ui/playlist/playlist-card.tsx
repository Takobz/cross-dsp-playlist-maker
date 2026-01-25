'use client'

import './playlist-card.css'
import { useDSPFromToSongsContext } from '@/app/hooks/context-hooks'
import { PlaylistItem } from '@/app/lib/definitions'
import { getAddToPlaylistFunction } from '@/app/utils/dsp-functions.util'
import { useDSPUserAccessToken } from '@/app/hooks/user-hooks'

type PlaylistCardProps = {
    playlistId: string
    playlistName: string,
    description: string,
    onAddItemComplete: (result: boolean) => void
}

const PlaylistCard = ({
    playlistId,
    playlistName,
    description,
    onAddItemComplete
} : PlaylistCardProps) => {
    const dspFromToSongsContext = useDSPFromToSongsContext();
    const dspUser = useDSPUserAccessToken(
        dspFromToSongsContext.dspFromToSongs.to
    );

    const onAddToPlaylist = async () => {
        const addToPlaylistFunction = getAddToPlaylistFunction(
            dspFromToSongsContext.dspFromToSongs.to
        );

        const items = dspFromToSongsContext.dspFromToSongs.toSongs.filter(s => s != null).map((song) => {
            return {
                ItemId: song.song_id.id
            } as PlaylistItem
        });

        const result = await addToPlaylistFunction(
            playlistId,
            dspUser?.AccessToken!,
            items
        );

        onAddItemComplete(result.isSuccess);
    }

    return (
        <div className="playlist-card-container">
            <h1>
                <strong>{playlistName}</strong>
            </h1>
            <p className="playlist-card-description ">{description}</p>

            <div className="playlist-add-button" onClick={onAddToPlaylist}>
                Add to Playlist
            </div>
        </div>
    );
}

export default PlaylistCard;