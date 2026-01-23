import { createContext, ReactNode, useState } from "react"
import { DSPUserData } from "../lib/cross-dsp-api-models"
import { AppStorage, LocalStorage } from "../utils/storage.util"

export type ContextProps = {
    children: ReactNode
}

export type DSPUsers = {
    GoogleUser?: DSPUserData,
    SpotifyUser?: DSPUserData
}

export type DSPUsersContextType = {
    dspUsers: DSPUsers,
    setDSPUsers: (dspUsers: DSPUsers) => void
}

const GOOGLE_USER_KEY = "GOOGLE_USER";
const SPOTIFY_USER_KEY = "SPOTIFY_USER";
const defaultDSPUsers = (storage: AppStorage) => {
    let dspUsers : DSPUsers = {};

    const googleUser = storage.getItemByKey<DSPUserData>(GOOGLE_USER_KEY);
    const spotifyUser = storage.getItemByKey<DSPUserData>(SPOTIFY_USER_KEY);

    if (googleUser.isSuccess){
        dspUsers.GoogleUser = googleUser.data!
    }

    if (spotifyUser.isSuccess){
        dspUsers.SpotifyUser = spotifyUser.data!
    }

    return dspUsers
}

const DSPUsersContext = createContext<DSPUsersContextType | undefined>(undefined);
const DSPUsersContextProvider = ({ children } : ContextProps  ) => {

    const storage = new LocalStorage();
    const defaultValue = defaultDSPUsers(storage);
    const [dspUsers, setDSPUsers] = useState(defaultValue);

    const defaultContextValue : DSPUsersContextType = {
        dspUsers: dspUsers,
        setDSPUsers: (dspUsers: DSPUsers) => {
            if (
                dspUsers.GoogleUser &&
                dspUsers.GoogleUser.user_name
            ) {
                storage.setItemByKey<DSPUserData>(
                    GOOGLE_USER_KEY,
                    dspUsers.GoogleUser!
                )
            }

            if (
                dspUsers.SpotifyUser &&
                dspUsers.SpotifyUser.user_name
            ) {
                storage.setItemByKey<DSPUserData>(
                    SPOTIFY_USER_KEY,
                    dspUsers.SpotifyUser!
                )
            }

            setDSPUsers(dspUsers)
        }
    }

    return (
        <DSPUsersContext.Provider value={defaultContextValue}>
            {children}
        </DSPUsersContext.Provider>
    )
}

export {
    DSPUsersContext,
    DSPUsersContextProvider
}

