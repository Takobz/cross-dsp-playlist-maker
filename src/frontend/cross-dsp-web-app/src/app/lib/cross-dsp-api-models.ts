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

export type DSPUserResponse = {
    data: DSPUserData,
    error_message: string[]
}

export type DSPUserData = {
    user_id: {
        dsp: SongDSP,
        id: string
    },
    user_name: string
}

export type DSPPlaylistsResponse = {
    data_items: DSPPlaylistData[],
    error_messages: string[]
}

export type DSPPlaylistResponse = {
    data: DSPPlaylistData,
    error_messages: string[]
}

export type DSPPlaylistData = {
    playlist_id: {
        dsp: string,
        id: string
    },
    playlist_name: string,
    playlist_discription: string
}

export type SongDSP = "YouTube Music" | "Spotify";