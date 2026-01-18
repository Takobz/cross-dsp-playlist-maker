'use client'

import { DSPNames } from "@/app/lib/definitions";
import "./DSPsong.css"

export type DSPSongProps = {
    mainArtistName: string,
    songTitle: string,
    dspName: DSPNames,
    songId: string,
    source: string
}

const DSPSong = (props: DSPSongProps) => {
    return (
        <div className="dsp-song-container">
            <div className="dsp-song-main-artist">
                {props.mainArtistName}
            </div>

            <div className="dsp-song-title">
                {props.songTitle}
            </div>

            <div className="dsp-song-source">
                {props.source}
            </div>
        </div>
    );
}

export default DSPSong;