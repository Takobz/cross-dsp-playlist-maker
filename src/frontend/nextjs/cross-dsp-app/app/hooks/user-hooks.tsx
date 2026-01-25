import { DSPNames } from "../lib/definitions";
import { useDSPAccessTokensContext, useDSPUsersContext } from "./context-hooks";

export const useDSPUser = (dspName: DSPNames) => {
    const dspUsersContext = useDSPUsersContext();
    switch (dspName) {
        case DSPNames.spotify:
            return dspUsersContext.dspUsers.SpotifyUser
        case DSPNames.ytmusic:
            return dspUsersContext.dspUsers.GoogleUser
    }
}

export const useDSPUserAccessToken = (dspName: DSPNames) => {
    const dspAccessTokensContext = useDSPAccessTokensContext();
    switch (dspName) {
        case DSPNames.spotify:
            return dspAccessTokensContext.dspTokens.Spotify
        case DSPNames.ytmusic:
            return dspAccessTokensContext.dspTokens.Google
    }
}