'use client'

import { supportedDSPFromToList } from './lib/default-data'
import SelectDSP from './ui/cards/selectDSP';

export default function Home() {

  return (
    <>
      <div className="flex min-h-screen items-center justify-center bg-zinc-50 font-sans dark:bg-black">
      <main className="flex min-h-screen w-full max-w-3xl flex-col items-center justify-between py-32 px-16 bg-white dark:bg-black sm:items-start">
        {supportedDSPFromToList.map(item => (
          <SelectDSP 
            key={item.key}
            fromImage={{
              src: item.from.src,
              alt: item.from.alt,
              dspName: item.from.dspName,
              dspDisplayName: item.from.dspDisplayName,
              height: item.from.height,
              width: item.from.width
            }}
            toImage={{
              src: item.to.src,
              alt: item.to.alt,
              dspName: item.to.dspName,
              dspDisplayName: item.to.dspDisplayName,
              height: item.to.height,
              width: item.to.width
            }}
            />
        ))}
      </main>
    </div>
    </>
  );
}
