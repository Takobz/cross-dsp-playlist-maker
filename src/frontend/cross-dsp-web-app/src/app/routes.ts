import { createBrowserRouter } from "react-router";
import App from "../App";
import AuthorizePage from "./authorize-init/page";

const routes = createBrowserRouter([
    {
        path: "/",
        Component: App
    },
    {
        path: "authorize-init",
        Component: AuthorizePage
    }
])

export default routes;