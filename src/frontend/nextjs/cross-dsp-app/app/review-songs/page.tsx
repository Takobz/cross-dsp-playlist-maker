'use client'

import { useContext } from "react";
import { DSPFromToSongsContext } from "../context/DSPFromToSongsContextProvider";

const ReviewSongs = () => {
    const dspFromToSongsContext = useContext(DSPFromToSongsContext);

    return (
        <>{dspFromToSongsContext?.dspFromToSongs && JSON.stringify(dspFromToSongsContext.dspFromToSongs)}</>
    );
}

export default ReviewSongs;