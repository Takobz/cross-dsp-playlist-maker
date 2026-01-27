'use client'

import type { DSPAuthReasons, ImageData } from "../../../lib/definitions";
import './selectDSP.css'
import { useNavigate } from "react-router";

export type SelectDSPImageProps = {
    fromImage: ImageData,
    toImage: ImageData
}

const SelectDSP = ({
    fromImage,
    toImage
}: SelectDSPImageProps
) => {
    const authReason : DSPAuthReasons = 'getFromSongs'
    const router = useNavigate(); 
    const onSelectDSPTile = () => {
        router(
            `authorize-init?dsp=${fromImage.dspName}&reason=${authReason}`
        );
    }

    return (
        <div 
            onClick={onSelectDSPTile} 
            className="select-dsp-container"
        >
            <div className="dsp-image-details">
                <img
                    src={fromImage.src}
                    alt={fromImage.alt}
                    width={fromImage.width}
                    height={fromImage.height}
                />
                <p>{fromImage.dspDisplayName}</p>
            </div>

            <p>To</p>

            <div className="dsp-image-details">
                <p>{toImage.dspDisplayName}</p>
                <img
                    src={toImage.src}
                    alt={toImage.alt}
                    width={toImage.width}
                    height={toImage.height}
                />
            </div>
        </div>
    );
}

export default SelectDSP;