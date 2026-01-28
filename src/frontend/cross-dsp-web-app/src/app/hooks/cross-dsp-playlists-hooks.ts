import { useEffect } from "react";
import type { DSPNames } from "../lib/definitions";
import type { DSPPlaylistsResponse } from "../lib/cross-dsp-api-models";
import { useDSPAccessTokensContext, useDSPUsersContext } from "./context-hooks";
import { getUserFunction, getUserPlaylistFunction } from "../utils/dsp-functions.util";

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
            let user = dspName === 'spotify' ?
              dspUsersContext.dspUsers.SpotifyUser :
              dspUsersContext.dspUsers.GoogleUser;

            const accessToken = dspName == 'spotify' ?
                dspAccessTokensContext.dspTokens.Spotify.AccessToken :
                dspAccessTokensContext.dspTokens.Google.AccessToken;

            if (user === undefined) {
                user = await userFunction(accessToken).then(result => {
                    return result.data
                })
                
                /*
                * Looks like a reducer can work well here
                */ 

                if (dspName === 'spotify') {
                    dspUsersContext.setDSPUsers(
                    {
                        ...dspUsersContext.dspUsers,
                        SpotifyUser: user
                    })
                }

                if (dspName === 'ytmusic') {
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