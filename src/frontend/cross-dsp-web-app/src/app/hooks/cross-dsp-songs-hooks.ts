import { useContext, useEffect, useState } from "react";
import type { DSPNames } from "../lib/definitions";
import type { DSPSongsResponse } from "../lib/cross-dsp-api-models";
import { DSPAccessTokensContext } from "../context/DSPAccessTokenContextProvider";
import { getGoogleSongsByQuery, getSpotifySongsByArtistAndName } from "../lib/cross-dsp-api-service";

export const useCrossDSPSongsFetcher = (
    query: {
        songName: string,
        artistName?: string
    },
    dspName: DSPNames
) => {
    const [songs, setSongs] = useState<DSPSongsResponse>()

    const dspAccessTokensContext = useContext(DSPAccessTokensContext);
    if (dspAccessTokensContext === undefined){
        throw Error(
            `${typeof(DSPAccessTokensContext)} should be used in components wrapped by the context provider`
        );
    }
    
    useEffect(() => {
        async function getSongs() {
            let songs : DSPSongsResponse;
            switch (dspName) {

                //TODO: move getting function to functions util
                case 'ytmusic':
                    songs = await getGoogleSongsByQuery(
                        query.songName,
                        dspAccessTokensContext?.dspTokens.Google.AccessToken!
                    )
                    setSongs(songs);
                    break;

                case 'spotify':
                    songs = await getSpotifySongsByArtistAndName(
                        query.songName,
                        query.artistName
                    );
                    setSongs(songs);
                    break;

                default:
                    throw new Error(`Provided DSP: ${dspName} has no resources handler`)
            }
        }

        getSongs();
    }, [query.songName, query.artistName, dspName]);

    return songs;
}