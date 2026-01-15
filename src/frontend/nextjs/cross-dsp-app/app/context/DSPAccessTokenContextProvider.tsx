"use client"

import { 
    createContext, 
    Dispatch, 
    ReactNode, 
    SetStateAction, 
    useContext, 
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
    setDSPTokens: Dispatch<SetStateAction<DSPAccessTokens>>
}

type ContextProps = {
    children: ReactNode
}

const defaultDSPAccessTokens = () : DSPAccessTokens => {
    //TODO have local storage read here.
    const dspTokens : DSPAccessTokens = {
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

    return dspTokens;
}
    
const DSPAccessTokensContext = createContext<DSPAccessTokensContext | undefined>(undefined);

const DSPAccessTokenContextProvider = ({ children } : ContextProps) => {

    const defaultTokens = defaultDSPAccessTokens();
    const [accessTokens, setAccessTokens] = useState<DSPAccessTokens>(defaultTokens);
    const providerContextData : DSPAccessTokensContext = {
        dspTokens: accessTokens,
        setDSPTokens: setAccessTokens
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