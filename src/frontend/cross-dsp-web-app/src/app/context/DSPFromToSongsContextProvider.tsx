import { createContext, type ReactNode, useState } from "react"
import { type DSPSongDataResponse } from "../../../../nextjs/cross-dsp-app/app/lib/cross-dsp-api-models"
import { DSPNames } from "../../../../nextjs/cross-dsp-app/app/lib/definitions"
import { type AppStorage, LocalStorage } from "../utils/storage.util"

export type ContextProps = {
    children: ReactNode
}

export type DSPFromToSongs = {
    from: DSPNames
    to: DSPNames
    fromSongs: DSPSongDataResponse[],
    toSongs: DSPSongDataResponse[]
}

export type DSPFromToSongsContext = {
    dspFromToSongs: DSPFromToSongs,
    setDSPFromToSongs: (fromToSongs: DSPFromToSongs) => void;
} 

const FROM_TO_KEY = "FROM-TO-DATA";
const defaultDSPFromToSongs = (storage: AppStorage) : DSPFromToSongs => {
    let fromToSongs : DSPFromToSongs = {
        from: DSPNames.ytmusic,
        to: DSPNames.spotify,
        fromSongs: [],
        toSongs: []
    } 

    const fromToResult = storage.getItemByKey<DSPFromToSongs>(FROM_TO_KEY);
    if (fromToResult.isSuccess) fromToSongs = fromToResult.data!;

    return fromToSongs;
}

const DSPFromToSongsContext = createContext<DSPFromToSongsContext | undefined>(undefined);

const DSPFromToSongsContextProvider = ({ children } : ContextProps) => {
    const storage = new LocalStorage();
    const [fromToSongs, setFromToSongs] = useState<DSPFromToSongs>(defaultDSPFromToSongs(storage));

    const setAndStoreDSPFromToSongs = (fromToSongs: DSPFromToSongs) => {
        storage.setItemByKey<DSPFromToSongs>(FROM_TO_KEY, fromToSongs);
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