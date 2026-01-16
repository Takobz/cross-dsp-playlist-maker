'use client'

import { useCrossDSPSongsFetcher } from "../hooks/cross-dsp-api-hooks"
import { DSPNames } from "../lib/definitions";

const SongPage = () => {
    const songs = useCrossDSPSongsFetcher(
        "jaded",
        DSPNames.ytmusic
    );

    return (

        <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
            <main className="flex min-h-screen w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start">
                {songs && songs.data_items ? songs.data_items.map(song => (
                    <div key={song.song_id.id}>
                        Main Artist: {song.main_artist_name}
                        Song Title: {song.song_title}
                        Source: {song.song_id.dsp}
                    </div>
                )) : <>No Songs</>}
            </main>
        </div>
    )
}

export default SongPage;