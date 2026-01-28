import { dspNameFormatter } from "../utils/dsp-formatter";
import DSPSong from "../ui/cards/dsp-song/DSPsong";
import Button from "../ui/buttons/button";
import { useContext } from "react";
import { type DSPSongsResponse } from "../lib/cross-dsp-api-models";
import { DSPFromToSongsContext } from "../../../../cross-dsp-web-app/src/app/context/DSPFromToSongsContextProvider";
import { useNavigate } from "react-router";
import { useCrossDSPSongsFetcher } from "../hooks/cross-dsp-songs-hooks";

const SongPage = () => {
    const router = useNavigate();
    const dspFromToSongsContext = useContext(DSPFromToSongsContext);
    const songs = useCrossDSPSongsFetcher(
        {
            songName: "jaded"
        },
        'ytmusic'
    );

    const onAddSongs = (songs: DSPSongsResponse | undefined) => {
        if (songs && songs.data_items) {
            dspFromToSongsContext?.setDSPFromToSongs({
                from: "ytmusic",
                to: "spotify",
                fromSongs: songs.data_items,
                toSongs: []
            });
            router("/review-songs");
        }
    }

    return (
        <div className="page-container">
            <main className="page-main">
                {songs && songs.data_items ? songs.data_items.map(song => (
                    <DSPSong 
                        key={song.song_id.id}
                        songId={song.song_id.id}
                        mainArtistName={song.main_artist_name}
                        songTitle={song.song_title}
                        source={song.song_id.dsp}
                        dspName={dspNameFormatter(song.song_id.dsp)}
                    />))
                    : <>No Songs</>
                }
                
                <div className="page-button">
                    {/*Add disabled when songs are not fetched yet*/}
                    <Button
                        text="Add"
                        onClick={() => onAddSongs(songs)}
                    />
                </div>
            </main>
        </div>
    );
}

export default SongPage;