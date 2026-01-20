'use client'

import { createContext, ReactNode, useState } from "react"
import { DSPSongDataResponse } from "../lib/cross-dsp-api-models"
import { DSPNames } from "../lib/definitions"
import { AppStorage, LocalStorage } from "@/utils/storage.util"

export type ContextProps = {
    children: ReactNode
}

export type DSPFromSongs = {
    from: DSPNames
    to: DSPNames
    songs: DSPSongDataResponse[]
}

export type DSPFromToSongsContext = {
    dspFromToSongs: DSPFromSongs,
    setDSPFromToSongs: (fromToSongs: DSPFromSongs) => void;
} 

const FROM_TO_KEY = "FROM-TO-DATA";
const defaultDSPFromToSongs = (storage: AppStorage) : DSPFromSongs => {
    let fromToSongs : DSPFromSongs = {
        from: DSPNames.ytmusic,
        to: DSPNames.spotify,
        songs: []
    } 

    const fromToResult = storage.getItemByKey<DSPFromSongs>(FROM_TO_KEY);
    if (fromToResult.isSuccess) fromToSongs = fromToResult.data!;

    return fromToSongs;
}

const DSPFromToSongsContext = createContext<DSPFromToSongsContext | undefined>(undefined);

const DSPFromToSongsContextProvider = ({ children } : ContextProps) => {
    const storage = new LocalStorage();
    const [fromToSongs, setFromToSongs] = useState<DSPFromSongs>(defaultDSPFromToSongs(storage));

    const setAndStoreDSPFromToSongs = (fromToSongs: DSPFromSongs) => {
        storage.setItemByKey<DSPFromSongs>(FROM_TO_KEY, fromToSongs);
        setFromToSongs(fromToSongs);
    }

    const initContexValue : DSPFromToSongsContext = {
        dspFromToSongs: fromToSongs,
        setDSPFromToSongs: setAndStoreDSPFromToSongs
    };

    return (
        <DSPFromToSongsContext.Provider value={initContexValue}>
            {children}
        </DSPFromToSongsContext.Provider>
    );
} 

export {
    DSPFromToSongsContext,
    DSPFromToSongsContextProvider
}