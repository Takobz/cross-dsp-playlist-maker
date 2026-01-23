// Util for getting different functions based on dsp name provided.

import { AuthorizationCodeFlowURLResponse, DSPAccessTokenResponse, DSPPlaylistsResponse, DSPSongsResponse, DSPUserResponse } from "../lib/cross-dsp-api-models";
import { getGoogleAccessToken, getGoogleRedirect, getSpotifyAccessToken, getSpotifyRedirect, getSpotifySongsByArtistAndName, getSpotifyUser, getSpotifyUserPlaylists } from "../lib/cross-dsp-api-service";
import { DSPNames } from "../lib/definitions";

export const getDSPToSongsFunction = (dspName: DSPNames) 
    : ((songName: string, artistName: string) => Promise<DSPSongsResponse>) => {
    switch (dspName) {
        case DSPNames.spotify:
            return getSpotifySongsByArtistAndName;
        default:
            throw Error(`DSP: ${dspName} has no to functions`);
    }
}

export const getDSPRedirectFunction = (dspName: DSPNames)
    : (() => Promise<AuthorizationCodeFlowURLResponse>) => {
    switch (dspName) {
        case DSPNames.ytmusic:
            return getGoogleRedirect;
        case DSPNames.spotify:
            return getSpotifyRedirect;
        default:
            throw new Error(`DSP: ${dspName} has no redirect functions`)
    }
}

export const getAccessTokenFunction = (dspName: DSPNames) 
    : ((authorizationState: string) => Promise<DSPAccessTokenResponse>) => {

    switch (dspName) {
        case DSPNames.ytmusic:
            return getGoogleAccessToken;
        case DSPNames.spotify:
            return getSpotifyAccessToken;
        default:
            throw new Error(`DSP: ${dspName} has no access token function`);
    }
}

export const getUserFunction = (dspName: DSPNames)
    : ((accessToken: string) => Promise<DSPUserResponse>) => {
    switch (dspName) {
        case DSPNames.spotify:
            return getSpotifyUser;
        default:
            throw new Error(`DSP: ${dspName} has no get user function`);
    }
}

export const getUserPlaylistFunction = (dspName: DSPNames)
    : ((userId: string, accessToken: string) => Promise<DSPPlaylistsResponse>) => {
    switch (dspName) {
        case DSPNames.spotify:
            return getSpotifyUserPlaylists;
        default:
            throw new Error(`DSP: ${dspName} has no get user function`);
    }
}
