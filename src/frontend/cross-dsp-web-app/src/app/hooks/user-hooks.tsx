import { type DSPNames } from "../lib/definitions";
import { useDSPAccessTokensContext, useDSPUsersContext } from "./context-hooks";

export const useDSPUser = (dspName: DSPNames) => {
    const dspUsersContext = useDSPUsersContext();
    switch (dspName) {
        case 'spotify':
            return dspUsersContext.dspUsers.SpotifyUser
        case 'ytmusic':
            return dspUsersContext.dspUsers.GoogleUser
    }
}

export const useDSPUserAccessToken = (dspName: DSPNames) => {
    const dspAccessTokensContext = useDSPAccessTokensContext();
    switch (dspName) {
        case 'spotify':
            return dspAccessTokensContext.dspTokens.Spotify
        case 'ytmusic':
            return dspAccessTokensContext.dspTokens.Google
    }
}