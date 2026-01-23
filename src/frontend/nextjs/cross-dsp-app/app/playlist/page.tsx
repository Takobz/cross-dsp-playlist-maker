'use client';

import { useRouter } from "next/navigation";
import { useDSPFromToSongsContext } from "../hooks/context-hooks";
import IconButton from "../ui/buttons/icon-button";

const PlaylistPage = () => {
    const router = useRouter();

    return (
        <div className="flex min-h-screen items-center justify-center">
            <main className="flex flex-col w-100 gap-10">
                <div>
                    <IconButton 
                        icon=""
                        text="Add To Existing Playlist"
                        onClick={() => router.push("playlist/list")}
                    />
                </div>
                 
                <div>
                    <IconButton 
                        icon=""
                        text="Add To New Playlist"
                        onClick={() => router.push("playlist/create")}
                    />
                </div>
                
            </main>
        </div>
    );
}

export default PlaylistPage;