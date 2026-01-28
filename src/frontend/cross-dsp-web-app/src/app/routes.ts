import { createBrowserRouter } from "react-router";
import App from "../App";
import AuthorizePage from "./authorize-init/page";
import SongPage from "./song-search/page";

const routes = createBrowserRouter([
    {
        path: "/",
        Component: App
    },
    {
        path: "authorize-init",
        Component: AuthorizePage
    },
    {
        path: "song-search",
        Component: SongPage
    }
])

export default routes;