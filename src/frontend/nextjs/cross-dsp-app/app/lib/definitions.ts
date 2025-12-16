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
    getRedirectFunction: () => Promise<AuthorizationCodeFlowURLResponse> 
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
