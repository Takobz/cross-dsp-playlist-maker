// Util for getting different functions based on dsp name provided.

import { 
    type AuthorizationCodeFlowURLResponse, 
    type DSPAccessTokenResponse, 
    type DSPPlaylistsResponse, 
    type DSPSongsResponse, 
    type DSPUserResponse 
} from "../lib/cross-dsp-api-models";

import { 
    getSpotifySongsByArtistAndName,
    addItemsToSpotifyPlaylist, 
    getGoogleAccessToken, 
    getGoogleRedirect, 
    getSpotifyAccessToken, 
    getSpotifyRedirect, 
    getSpotifyUser, 
    getSpotifyUserPlaylists 
} from "../lib/cross-dsp-api-service";

import type { 
    AddPlaylistItemResult, 
    DSPNames, 
    PlaylistItem 
} from "../lib/definitions";

export const getDSPToSongsFunction = (dspName: DSPNames) 
    : ((songName: string, artistName: string) => Promise<DSPSongsResponse>) => {
    switch (dspName) {
        case 'spotify':
            return getSpotifySongsByArtistAndName;
        default:
            throw Error(`DSP: ${dspName} has no to functions`);
    }
}

export const getDSPRedirectFunction = (dspName: DSPNames)
    : (() => Promise<AuthorizationCodeFlowURLResponse>) => {
    switch (dspName) {
        case 'ytmusic':
            return getGoogleRedirect;
        case 'spotify':
            return getSpotifyRedirect;
        default:
            throw new Error(`DSP: ${dspName} has no redirect functions`)
    }
}

export const getAccessTokenFunction = (dspName: DSPNames) 
    : ((authorizationState: string) => Promise<DSPAccessTokenResponse>) => {

    switch (dspName) {
        case 'ytmusic':
            return getGoogleAccessToken;
        case 'spotify':
            return getSpotifyAccessToken;
        default:
            throw new Error(`DSP: ${dspName} has no access token function`);
    }
}

export const getUserFunction = (dspName: DSPNames)
    : ((accessToken: string) => Promise<DSPUserResponse>) => {
    switch (dspName) {
        case 'spotify':
            return getSpotifyUser;
        default:
            throw new Error(`DSP: ${dspName} has no get user function`);
    }
}

export const getUserPlaylistFunction = (dspName: DSPNames)
    : ((userId: string, accessToken: string) => Promise<DSPPlaylistsResponse>) => {
    switch (dspName) {
        case 'spotify':
            return getSpotifyUserPlaylists;
        default:
            throw new Error(`DSP: ${dspName} has no get user function`);
    }
}

export const getAddToPlaylistFunction = (dspName: DSPNames)
    : ((playlistId: string, accessToken: string, items: PlaylistItem[]) => Promise<AddPlaylistItemResult>) => {
    switch(dspName) {
        case 'spotify':
            return addItemsToSpotifyPlaylist;
        default:
            throw new Error(`DSP: ${dspName} has no add to list function`)
    }
}
