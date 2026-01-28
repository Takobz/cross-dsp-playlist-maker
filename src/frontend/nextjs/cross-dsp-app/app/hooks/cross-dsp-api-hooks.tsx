import { useContext, useEffect, useState } from "react";
import { DSPSongDataResponse, DSPSongsResponse } from "../lib/cross-dsp-api-models";
import { getGoogleSongsByQuery, getSpotifySongsByArtistAndName } from "../lib/cross-dsp-api-service"
import { DSPNames } from "../lib/definitions"
import { DSPAccessTokensContext } from "@/app/context/DSPAccessTokenContextProvider"
import { DSPFromToSongsContext } from "../context/DSPFromToSongsContextProvider";
import { getAccessTokenFunction, getDSPRedirectFunction, getDSPToSongsFunction } from "../utils/dsp-functions.util";

const useCrossDSPAuthorization = (dspName: DSPNames) => {
    return getDSPRedirectFunction(dspName);
}

const useCrossDSPSongsFetcher = (
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
                case DSPNames.ytmusic:
                    songs = await getGoogleSongsByQuery(
                        query.songName,
                        dspAccessTokensContext?.dspTokens.Google.AccessToken!
                    )
                    setSongs(songs);
                    break;

                case DSPNames.spotify:
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

/**
 * Gets correlating songs from one DSP to another.  
 * Correlates songs from different DSPs using song name and artist name.  
 * Uses DSPFromToSongsContext internally to find from songs and to function.
 * @param onComplete - Listener to pass when fetching all songs is done.
 */
const useCrossDSPGetToSongs = (onComplete: (results: DSPSongsResponse[]) => void) => {
    const dspFromToSongsContext = useContext(DSPFromToSongsContext);
    if (dspFromToSongsContext === undefined || dspFromToSongsContext === null)
        throw Error("use DSPFromToSongsContext in component wrapped by correct provider");

    const toDSPName = dspFromToSongsContext?.dspFromToSongs.to!;
    const getToSongsFunc = getDSPToSongsFunction(toDSPName);

    useEffect(() => {
        async function getToSongs(){
            if (dspFromToSongsContext?.dspFromToSongs) {
                let promises :Promise<DSPSongsResponse>[] = [];
                const fromSongs = dspFromToSongsContext.dspFromToSongs.fromSongs;
                for (let i = 0; i < fromSongs.length; i ++){
                    promises.push(getToSongsFunc(
                        fromSongs[i].song_title,
                        fromSongs[i].main_artist_name
                    ))
                }

                try {
                    const results = await Promise.all(promises);
                    onComplete(results);
                    setDefaultSongsToAddToPlaylist(
                        results,
                        dspFromToSongsContext
                    );
                }
                catch(err) {
                    console.log(err);
                }
            }
        }

        if (dspFromToSongsContext?.dspFromToSongs) {
            getToSongs();
        }

    }, [dspFromToSongsContext?.dspFromToSongs.fromSongs]);

    return { 
        fromDSPName: dspFromToSongsContext.dspFromToSongs.from,
        toDSPName: dspFromToSongsContext.dspFromToSongs.to
    }
}

let INTERVAL_ID: NodeJS.Timeout;
const useDSPAccessTokenPoller = (
    dspName: DSPNames, 
    authorizationState: string,
    onPollingComplete: () => void
) => {
    const dspAccessTokensContext = useContext(DSPAccessTokensContext);
    if (dspAccessTokensContext === undefined){
        throw Error(
            `${typeof(DSPAccessTokensContext)} should be used in components wrapped by the context provider`
        );
    }

    useEffect(() => {
        const delayInMilliSeconds = 10000;
        INTERVAL_ID = setInterval(
            accessTokenPoller,
            delayInMilliSeconds,  
            dspName, 
            authorizationState,
            dspAccessTokensContext,
            onPollingComplete
        );
    }, [dspName, authorizationState]);
}

const accessTokenPoller = async (
    dspName: DSPNames, 
    authorizationState: string,
    dspAccessTokenContext: DSPAccessTokensContext,
    onPollComplete: () => void
) => {      

    await setDSPAccessToken(
        dspName, 
        authorizationState,
        dspAccessTokenContext!,
        onPollComplete
    );
};


/**INTERNAL METHODS**/

const setDefaultSongsToAddToPlaylist = (
    toSongs: DSPSongsResponse[],
    dspFromToSongsContext: DSPFromToSongsContext
) => {
    const defaultSongs = toSongs.map((songsMatched) => {
        if (songsMatched.data_items.length > 0) {
            return songsMatched.data_items[0]
        }
    });

    const currentDSPFromToSongs = dspFromToSongsContext.dspFromToSongs;
    if (defaultSongs !== undefined) {
        dspFromToSongsContext.setDSPFromToSongs({
            ...currentDSPFromToSongs,
            toSongs: defaultSongs as DSPSongDataResponse[]
        })
    }
}

async function setDSPAccessToken(
    dspName: DSPNames, 
    authorizationState: string,
    dspAccessTokensContext: DSPAccessTokensContext,
    onPollComplete: () => void
) {
    const currentTokens = dspAccessTokensContext.dspTokens;
    const accessTokenFunc = getAccessTokenFunction(dspName);
    const token = await accessTokenFunc(authorizationState);

    if (token.data) {
        clearInterval(INTERVAL_ID);

        switch(dspName){
            case DSPNames.ytmusic:
                dspAccessTokensContext.setDSPTokens(
                {
                    ...currentTokens, 
                    Google: {
                        AccessToken: token.data.access_token,
                        RefreshToken: token.data.refresh_token,
                        ExpiresIn: token.data.expires_in
                    }
                });
                onPollComplete();
                break;

            case DSPNames.spotify:
                dspAccessTokensContext.setDSPTokens({
                    ...currentTokens,
                    Spotify: {
                        AccessToken: token.data.access_token,
                        RefreshToken: token.data.refresh_token,
                        ExpiresIn: token.data.expires_in
                    }
                })
                onPollComplete();
                break;

            default:
                throw new Error(`Provided DSP: ${dspName} not supported.`);
        }
    }

    if (token.error_messages.length > 0) {
        //will need to prop this to the user some how.
        console.log(token.error_messages)
    }
}

export { 
    useCrossDSPAuthorization,
    useDSPAccessTokenPoller,
    useCrossDSPSongsFetcher,
    useCrossDSPGetToSongs
}