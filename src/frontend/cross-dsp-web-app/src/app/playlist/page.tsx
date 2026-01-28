import "./page.css"
import { useNavigate } from "react-router";
import Button from "../ui/buttons/button";

const PlaylistPage = () => {
    const router = useNavigate();

    return (
        <div className="playlist-page-container">
            <main className="playlist-page-main">
                <div>
                    <Button
                        text="Add To Existing Playlist"
                        onClick={() => router("/playlist/list")}
                    />
                </div>
                 
                <div>
                    <Button
                        text="Add To New Playlist"
                        onClick={() => router("/playlist/create")}
                    />
                </div>
                
            </main>
        </div>
    );
}

export default PlaylistPage;