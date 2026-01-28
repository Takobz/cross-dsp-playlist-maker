import "./page.css"
import { useState } from "react";
import { type DSPSongsResponse } from "../lib/cross-dsp-api-models";
import { useNavigate } from "react-router";
import { useCrossDSPGetToSongs } from "../hooks/cross-dsp-songs-hooks";
import type { DSPAuthReasons } from "../lib/definitions";
import DSPSong from "../ui/cards/dsp-song/DSPsong";
import { dspNameFormatter } from "../utils/dsp-formatter";
import Button from "../ui/buttons/button";


const ReviewSongs = () => {
    const [toSongs, setToSongs] = useState<DSPSongsResponse[]>();
    const router = useNavigate();

    const { toDSPName } = useCrossDSPGetToSongs(
        (results: DSPSongsResponse[]) => setToSongs(results)
    );

    const onSongsAdd = () => {
        const authReason: DSPAuthReasons = 'getToSongs';
        router(`/authorize-init?dsp=${toDSPName}&reason=${authReason}`);
    }

    return (
        <>
            <div className="page-container">
                <main className="page-main">
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

                    <div className="page-button">
                        {/*Add disabled when songs are not fetched yet*/}
                        <Button
                            text="Add"
                            onClick={() => onSongsAdd()}
                        />
                    </div>
                </main>
            </div>
        </>
    );
}

export default ReviewSongs;