'use client'

import { useContext, useEffect, useState } from "react";
import { DSPFromToSongsContext } from "../context/DSPFromToSongsContextProvider";
import { DSPSongsResponse } from "../lib/cross-dsp-api-models";
import { getSpotifySongsByArtistAndName } from "../lib/cross-dsp-api-service";

const ReviewSongs = () => {
    const dspFromToSongsContext = useContext(DSPFromToSongsContext);
    const [toSongs, setToSongs] = useState<DSPSongsResponse[]>();

    /*
    * TODO:
    * Abstract this to a hook
    * let hook take list of data
    * an async function to call for each data item
    * returns required data (generic hook).
    */
    useEffect(() => {
        async function getToSongs(){
            if (dspFromToSongsContext?.dspFromToSongs) {
                let promises :Promise<DSPSongsResponse>[] = [];
                const fromSongs = dspFromToSongsContext.dspFromToSongs.songs;
                for (let i = 0; i < fromSongs.length; i ++){
                    promises.push(getSpotifySongsByArtistAndName(
                        fromSongs[i].song_title,
                        fromSongs[i].main_artist_name
                    ))
                }

                try {
                    const results = await Promise.all(promises);
                    setToSongs(results);
                }
                catch(err) {
                    console.log(err);
                }
            }
        }

        if (dspFromToSongsContext?.dspFromToSongs) {
            getToSongs();
        }

    }, [dspFromToSongsContext?.dspFromToSongs]);

    return (
        <>{toSongs && JSON.stringify(toSongs)}</>
    );
}

export default ReviewSongs;