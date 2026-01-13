"use client"

import { Children, createContext, ReactNode } from "react"

export type DSPAccessToken = {
    AccessToken: string,
    RefreshToken: string,
    ExpiresIn: number
}

export type DSPAccessTokens = {
    Google: DSPAccessToken,
    Spotify: DSPAccessToken
}

type ContextProps = {
    children: ReactNode
}

const DSPAccessTokenContextProvider = ({ children } : ContextProps) => {
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
    
    const AccessTokensContext = createContext(defaultDSPAccessTokens())

    return (
        <AccessTokensContext value={defaultDSPAccessTokens()}>
            {children}
        </AccessTokensContext>
    );
}

export default DSPAccessTokenContextProvider