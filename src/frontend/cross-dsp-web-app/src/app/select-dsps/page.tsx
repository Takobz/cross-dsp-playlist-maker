import { supportedDSPFromToList } from "../lib/default-data";
import SelectDSP from "../ui/cards/select-dsp/selectDSP";

const SelectDSPs = () => {
    return (
        <>
            {supportedDSPFromToList.map((fromToItem) =>
                <SelectDSP
                    key={fromToItem.key}
                    fromImage={fromToItem.from}
                    toImage={fromToItem.to}
                />
            )}
        </>
    );
}

export default SelectDSPs;