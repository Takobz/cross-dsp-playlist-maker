import { createBrowserRouter } from "react-router";
import App from "../App";
import AuthorizePage from "./authorize-init/page";
import SongPage from "./song-search/page";
import ReviewSongs from "./review-songs/page";

const routes = createBrowserRouter([
    {
        path: "/",
        Component: App
    },
    {
        path: "/authorize-init",
        Component: AuthorizePage
    },
    {
        path: "/song-search",
        Component: SongPage
    },
    {
        path: "/review-songs",
        Component: ReviewSongs
    }
])

export default routes;