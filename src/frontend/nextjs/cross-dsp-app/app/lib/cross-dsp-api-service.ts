import { AuthorizationCodeFlowURLResponse } from "./cross-dsp-api-models";

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

