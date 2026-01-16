import { 
    AuthorizationCodeFlowURLResponse, 
    DSPAccessTokenResponse, 
    DSPSongsResponse
} from "./cross-dsp-api-models";

export async function getGoogleRedirect(): Promise<AuthorizationCodeFlowURLResponse> {
    return await fetch(`dsp-api/auth/google-init`)
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
    return await fetch(`dsp-api/auth/google-token?authorization_state=${authorizationState}`)
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

    return await fetch(`dsp-api/google/search?query=${query}`, {
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
