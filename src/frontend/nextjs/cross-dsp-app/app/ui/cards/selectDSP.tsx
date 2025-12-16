'use client'
import { SelectDSPImageData } from "@/app/lib/definitions";
import Image from "next/image";
import './selectDSP.css'
import { ChevronDoubleRightIcon } from "@heroicons/react/24/outline";

const SelectDSP = ({
    fromImage,
    toImage
}: SelectDSPImageData
) => {
    return (
        <div className="select-dsp-container">
            <div className="dsp-image-details">
                <Image
                    src={fromImage.src}
                    alt={fromImage.alt}
                    width={fromImage.width}
                    height={fromImage.height}
                />
                <p>YouTube Music</p>
            </div>

            <p>To</p>

            <div className="dsp-image-details">
                <p>Spotify</p>
                <Image
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