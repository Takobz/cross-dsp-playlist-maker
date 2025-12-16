'use client'

import { SelectDSPImageProps } from "@/app/lib/definitions";
import Image from "next/image";
import './selectDSP.css'
import { useRouter } from "next/navigation";

const SelectDSP = ({
    fromImage,
    toImage
}: SelectDSPImageProps
) => {
    const router = useRouter(); 
    const onSelectDSPTile = () => {
        router.push(
            `authorize-init?from=${fromImage.dspName}&to=${toImage.dspName}`
        );
    }

    return (
        <div 
            onClick={onSelectDSPTile} 
            className="select-dsp-container"
        >
            <div className="dsp-image-details">
                <Image
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