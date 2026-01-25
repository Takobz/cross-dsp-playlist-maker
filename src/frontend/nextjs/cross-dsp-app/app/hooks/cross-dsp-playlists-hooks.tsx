'use client'

import { useEffect } from "react";
import { DSPNames, PlaylistItem } from "../lib/definitions";
import { useDSPAccessTokensContext, useDSPUsersContext } from "./context-hooks";
import { getAddToPlaylistFunction, getUserFunction, getUserPlaylistFunction } from "../utils/dsp-functions.util";
import { DSPPlaylistsResponse, DSPSongDataResponse } from "../lib/cross-dsp-api-models";
import { useDSPUser, useDSPUserAccessToken } from "./user-hooks";
import { DSPAccessTokens } from "../context/DSPAccessTokenContextProvider";

export const useUserDSPPlaylists = (
    dspName: DSPNames,
    onComplete: (result: DSPPlaylistsResponse) => void
    // pageFrom: number, for future use to paginate
    // pageTo: number
) => {
    const dspAccessTokensContext = useDSPAccessTokensContext();
    const dspUsersContext = useDSPUsersContext();

    const userFunction = getUserFunction(dspName);
    const userPlaylistFunction = getUserPlaylistFunction(dspName);

    useEffect(() => {
        async function getUserPlaylists() {
            let user = dspName === DSPNames.spotify ?
              dspUsersContext.dspUsers.SpotifyUser :
              dspUsersContext.dspUsers.GoogleUser;

            const accessToken = dspName == DSPNames.spotify ?
                dspAccessTokensContext.dspTokens.Spotify.AccessToken :
                dspAccessTokensContext.dspTokens.Google.AccessToken;

            if (user === undefined) {
                user = await userFunction(accessToken).then(result => {
                    return result.data
                })
                
                /*
                * Looks like a reducer can work well here
                */ 

                if (dspName === DSPNames.spotify) {
                    dspUsersContext.setDSPUsers(
                    {
                        ...dspUsersContext.dspUsers,
                        SpotifyUser: user
                    })
                }

                if (dspName === DSPNames.ytmusic) {
                    dspUsersContext.setDSPUsers(
                    {
                        ...dspUsersContext.dspUsers,
                        GoogleUser: user
                    })
                }
                
            }

            const playlists = await userPlaylistFunction(
                user?.user_id.id!,
                accessToken
            )

            onComplete(playlists);
        }

        getUserPlaylists();
    }, []);
}