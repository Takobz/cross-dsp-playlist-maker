import { type AppStorage, LocalStorage } from "../utils/storage.util"
import type { ReactNode } from "react"
import { 
    createContext, 
    useState 
} from "react"


export type DSPAccessToken = {
    AccessToken: string,
    RefreshToken: string,
    ExpiresIn: number
}

export type DSPAccessTokens = {
    Google: DSPAccessToken,
    Spotify: DSPAccessToken
}

export type DSPAccessTokensContext = {
    dspTokens: DSPAccessTokens,
    setDSPTokens: (tokens: DSPAccessTokens) => void
}

type ContextProps = {
    children: ReactNode
}

const GOOGLE_KEY = "GOOGLE-TOKEN";
const SPOTIFY_KEY = "SPOTIFY-TOKEN";

/**
 * Sets default values for DSPAccessTokensContext.
 * Checks if local storage has the values and set those as default if they exist.
 * 
 * @param storage - AppStorage inteface with local storage implementation.
 * @returns DSPAccessTokens from the local storage if they exist, otherwise empty values
 */
const defaultDSPAccessTokens = (storage: AppStorage) : DSPAccessTokens => {
    let dspTokens : DSPAccessTokens = {
        Google: {
            AccessToken: "",
            RefreshToken: "",
            ExpiresIn: 0
        },
        Spotify: {
            AccessToken: "",
            RefreshToken: "",
            ExpiresIn: 0
        }
    };

    const googleToken = storage.getItemByKey<DSPAccessToken>(GOOGLE_KEY);
    const spotifyToken = storage.getItemByKey<DSPAccessToken>(SPOTIFY_KEY);

    if (googleToken.isSuccess) dspTokens.Google = googleToken.data!
    if (spotifyToken.isSuccess) dspTokens.Spotify = spotifyToken.data!

    return dspTokens;
}
    
const DSPAccessTokensContext = createContext<DSPAccessTokensContext | undefined>(undefined);

/**
* React component that provides the DSP Access Tokens to the children components.
* The component uses createContext to create the context DSPAccessTokenContext.
* The provider internally stores the tokens locally in the local storage.
* 
* @param children - ReactNode that represent a component that is usually root of all components that need the dsp access token.
*/
const DSPAccessTokenContextProvider = ({ children } : ContextProps) => {

    const storage = new LocalStorage();
    const defaultTokens = defaultDSPAccessTokens(storage);
    const [accessTokens, setAccessTokens] = useState<DSPAccessTokens>(defaultTokens);
    const setAndStoreDSPTokens = (tokens: DSPAccessTokens) => {
        if (
            tokens.Google.AccessToken && 
            tokens.Google.AccessToken !== ""
        ) {
            storage.setItemByKey(
                GOOGLE_KEY,
                tokens.Google 
            );
        }

        if (
            tokens.Spotify.AccessToken &&
            tokens.Spotify.AccessToken !== ""
        ) {
            storage.setItemByKey(
                SPOTIFY_KEY,
                tokens.Spotify 
            );
        }

        setAccessTokens(tokens);
    }

    const providerContextData : DSPAccessTokensContext = {
        dspTokens: accessTokens,
        setDSPTokens: setAndStoreDSPTokens
    }

    return (
        <DSPAccessTokensContext.Provider value={providerContextData}>
            {children}
        </DSPAccessTokensContext.Provider>
    );
}

export { 
    DSPAccessTokenContextProvider,
    DSPAccessTokensContext
 }