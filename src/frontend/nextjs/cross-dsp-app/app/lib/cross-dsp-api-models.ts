export type AuthorizationCodeFlowURLResponse = {
    data: {
        authorization_code_flow_redirect: string;
        authorization_state: string;
    },
    error_messages: string[];
}

export type DSPAccessTokenResponse = {
    data: {
        access_token: string;
        refresh_token: string;
        expires_in: number;
    },
    error_messages: string[];
}

export type DSPSongDataResponse = {
    main_artist_name: string,
    song_title: string,
    song_id: {
        dsp: SongDSP,
        id: string
    }
}

export type DSPSongsResponse = {
    data_items: DSPSongDataResponse[],
    error_messages: []
}

export enum SongDSP {
    ytmusic = "YouTube Music",
    spotify = "Spotify"
}