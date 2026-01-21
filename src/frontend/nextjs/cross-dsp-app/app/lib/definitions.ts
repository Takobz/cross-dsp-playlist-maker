import { AuthorizationCodeFlowURLResponse } from "./cross-dsp-api-models"

export type ImageData = {
    src: string,
    alt: string,
    dspName: DSPNames
    dspDisplayName: DSPDisplayNames
    width: number
    height: number
}

export type SelectDSPImageProps = {
    fromImage: ImageData,
    toImage: ImageData
}

export interface LoadRedirectProps {
    getRedirectFunction: () => Promise<AuthorizationCodeFlowURLResponse> ,
    dspName: DSPNames,
    authReason: DSPAuthReasons
}

export enum DSPNames {
    ytmusic = 'ytmusic',
    spotify = 'spotify',
    soundcloud = 'soundcloud',
    applemusic = 'applemusic'
}

export enum DSPDisplayNames {
    ytmusic = 'YouTube Music',
    spotify = 'Spotify'
}

/**
 * Enum to help the FollowRedirect component know where to take you after auth.    
 * getFromSongs will redirect to song-search page.  
 * getToSongs will redirect to playlist/create page
 */
export enum DSPAuthReasons {
    getFromSongs = 'getFromSongs',
    getToSongs = 'getToSongs'
}
