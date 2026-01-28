import { createBrowserRouter } from "react-router";
import App from "../App";
import AuthorizePage from "./authorize-init/page";
import SongPage from "./song-search/page";
import ReviewSongs from "./review-songs/page";
import PlaylistPage from "./playlist/page";
import PlaylistList from "./playlist/list/page";
import PlaylistCreate from "./playlist/create/page";

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
    },
    {
        path: "/playlist",
        Component: PlaylistPage
    },
    {
        path: "playlist/list",
        Component: PlaylistList
    },
    {
        path: "playlist/create",
        Component: PlaylistCreate
    }
])

export default routes;