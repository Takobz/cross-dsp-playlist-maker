import type { AuthorizationCodeFlowURLResponse } from "./cross-dsp-api-models"

export type ImageData = {
    src: string,
    alt: string,
    dspName: DSPNames
    dspDisplayName: DSPDisplayNames
    width: number
    height: number
}

export interface LoadRedirectProps {
    getRedirectFunction: () => Promise<AuthorizationCodeFlowURLResponse> ,
    dspName: DSPNames,
    authReason: DSPAuthReasons
}

export interface PlaylistItem {
    ItemId: string
}

export interface AddPlaylistItemResult {
    isSuccess: boolean;
}

export type DSPNames = 'ytmusic' | 'spotify' | 'soundcloud' | 'applemusic'

export type DSPDisplayNames = 'YouTube Music' | 'Spotify';

/**
 * Enum to help the FollowRedirect component know where to take you after auth.    
 * getFromSongs will redirect to song-search page.  
 * getToSongs will redirect to playlist/create page
 */
export type DSPAuthReasons = 'getFromSongs' | 'getToSongs'