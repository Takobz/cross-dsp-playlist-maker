import type { 
    AuthorizationCodeFlowURLResponse, 
    DSPAccessTokenResponse, 
    DSPPlaylistsResponse, 
    DSPSongsResponse,
    DSPUserResponse
} from "./cross-dsp-api-models";
import type { AddPlaylistItemResult, PlaylistItem } from "./definitions";

/*
 * I know functions in here have the same signature and URLs are the only thing that is dynamic.
 * I will work the strength to refactor these just putting this comment here for my sanity.  
 */

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export async function getGoogleRedirect(): Promise<AuthorizationCodeFlowURLResponse> {
    return await fetch(`${API_BASE_URL}/auth/google-init`)
        .then(response => {
            if (response.ok) {
                return response.json()
            }
            else {
                console.log(response);
                throw Error("Failed To Get Google Token")
            }
        })
        .then(apiResponse => {
            return apiResponse as AuthorizationCodeFlowURLResponse
        }
    );
}

export async function getGoogleAccessToken(authorizationState: string): Promise<DSPAccessTokenResponse> {
    return await fetch(`${API_BASE_URL}/auth/google-token?authorization_state=${authorizationState}`)
        .then(response => {
            if (response.ok){
                return response.json();
            }
            else {
                console.log(response);
                throw new Error("Failed to get Google Token");
            }
        })
        .then(apiResponse => {
            return apiResponse as DSPAccessTokenResponse;
        }
    );
}

export async function getGoogleSongsByQuery(
    query: string,
    beaerToken: string
): Promise<DSPSongsResponse>{

    return await fetch(`${API_BASE_URL}/google/search?query=${query}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${beaerToken}`
        }
    })
    .then(response => {
        if (response.ok){
            return response.json()
        }
        else {
            console.log(response);
            throw new Error("Failed to data from api");
        }
    })
    .then(apiResponse => apiResponse as DSPSongsResponse)
}

export async function getSpotifyRedirect(): Promise<AuthorizationCodeFlowURLResponse> {
    return await fetch(`${API_BASE_URL}/auth/spotify-init`)
        .then(response => {
            if (response.ok) {
                return response.json()
            }
            else {
                console.log(response);
                throw Error("Failed To Get Google Token")
            }
        })
        .then(apiResponse => {
            return apiResponse as AuthorizationCodeFlowURLResponse
        }
    );
}

export async function getSpotifyAccessToken(authorizationState: string): Promise<DSPAccessTokenResponse> {
    return await fetch(`${API_BASE_URL}/auth/spotify-token?authorization_state=${authorizationState}`)
        .then(response => {
            if (response.ok){
                return response.json();
            }
            else {
                console.log(response);
                throw new Error("Failed to get Google Token");
            }
        })
        .then(apiResponse => {
            return apiResponse as DSPAccessTokenResponse;
        }
    );
}

export async function getSpotifySongsByArtistAndName(
    songName: string,
    artistName?: string
): Promise<DSPSongsResponse> {
    const queryParams = artistName === undefined ?
        `song_name=${songName}` : `artist_name=${artistName}&song_name=${songName}`;
        
    return await fetch(`${API_BASE_URL}/spotify/search/song?${queryParams}`,
        {
            method: "GET"
        })
        .then(response => {
            if (response.ok) {
                return response.json()
            }
            else {
                console.log(response);
                throw Error("Failed To Get Google Token")
            }
        })
        .then(apiResponse => {
            return apiResponse as DSPSongsResponse
        }
    );
}

export async function getSpotifyUser(accessToken: string) : Promise<DSPUserResponse> {
    return await fetch(`${API_BASE_URL}/spotify/user`, 
        {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${accessToken}`
            }
        })
        .then(response => {
            if (response.ok){
                return response.json();
            } else {
                console.log(response);
                throw Error("Failed To Get Spotify User")
            }
        })
        .then(apiResponse => {
            return apiResponse as DSPUserResponse
        }
    );
}

export async function getSpotifyUserPlaylists(
    userId: string,
    accessToken: string
) : Promise<DSPPlaylistsResponse>{
    return await fetch(`${API_BASE_URL}/spotify/user/${userId}/playlists`, 
        {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${accessToken}`
            }
        })
        .then(response => {
            if (response.ok){
                return response.json();
            } else {
                console.log(response);
                throw Error("Failed To Get Spotify User Playlists")
            }
        })
        .then(apiResponse => {
            return apiResponse as DSPPlaylistsResponse
        }
    );
}

export async function addItemsToSpotifyPlaylist(
    playlistId: string,
    accessToken: string,
    items: PlaylistItem[]
) : Promise<AddPlaylistItemResult> {
    const songIds = items.map((item) => item.ItemId);
    return await fetch(`${API_BASE_URL}/spotify/playlists/${playlistId}`, 
        {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${accessToken}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(songIds)
        })
        .then(response => {
            const result = {
                isSuccess: response.ok
            }

            if (!result.isSuccess) console.log(response);

            return result;
        }
    );
}
