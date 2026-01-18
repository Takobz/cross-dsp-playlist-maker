'use client'

import { dspNameFormatter } from "@/utils/dsp-formatter";
import { useCrossDSPSongsFetcher } from "../hooks/cross-dsp-api-hooks"
import { DSPNames } from "../lib/definitions";
import DSPSong from "../ui/cards/DSPsong";
import IconButton from "../ui/buttons/icon-button";

const SongPage = () => {
    const songs = useCrossDSPSongsFetcher(
        "jaded",
        DSPNames.ytmusic
    );

    return (

        <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
            <main className="scrollable-containerflex min-h-screen w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start">
                {songs && songs.data_items ? songs.data_items.map(song => (
                    <DSPSong 
                        key={song.song_id.id}
                        songId={song.song_id.id}
                        mainArtistName={song.main_artist_name}
                        songTitle={song.song_title}
                        source={song.song_id.dsp}
                        dspName={dspNameFormatter(song.song_id.dsp)}
                    />


                )): <>No Songs</>
                }

                
                {/* <IconButton 
                    icon=""
                    text="Add"
                    onClick={() => alert('Clicked Btn')}
                /> */}
            </main>
        </div>
    )
}



export default SongPage;