'use client'

import { useState } from "react";
import { DSPSongsResponse } from "../lib/cross-dsp-api-models";
import DSPSong from "../ui/cards/DSPsong";
import { dspNameFormatter } from "@/app/utils/dsp-formatter";
import IconButton from "../ui/buttons/icon-button";
import { useCrossDSPGetToSongs } from "../hooks/cross-dsp-api-hooks";

const ReviewSongs = () => {
    const [toSongs, setToSongs] = useState<DSPSongsResponse[]>();

    useCrossDSPGetToSongs(
        (results: DSPSongsResponse[]) => setToSongs(results)
    );

    return (
        <>
            <div className="flex min-h-screen items-center justify-center">
                <main className="w-full max-w-3xl sm:items-start">
                    {toSongs ? toSongs.map((songsMatched, index) => {
                        if (songsMatched.data_items.length > 0) {
                            const song = songsMatched.data_items[0]
                            return (
                                <DSPSong 
                                    key={index}
                                    songId={song.song_id.id}
                                    songTitle={song.song_title}
                                    mainArtistName={song.main_artist_name}
                                    source={song.song_id.dsp}
                                    dspName={dspNameFormatter(song.song_id.dsp)}
                                />
                            );
                        }}) : <>Getting Matching songs</>
                    }

                    <div className="flex items-center justify-center w-full m-6">
                        {/*Add disabled when songs are not fetched yet*/}
                        <IconButton 
                            icon=""
                            text="Add"
                            onClick={() => alert("Click click... Boom!")}
                        />
                    </div>
                </main>
            </div>
        </>
    );
}

export default ReviewSongs;