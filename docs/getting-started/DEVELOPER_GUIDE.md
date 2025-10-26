# Developer Guide.

This guide goes through how to get started as a developer, if you want to contribute.  
The guide will go through how get the app running locally.

## Pre-Requsites Needed.
The following software(s) are needed to get the app running in docker.

> [!NOTE]
> If you have installed Docker Desktop, Docker CLI and Docker Compose will be installed.
> There's no need for installing them as saparate components. 

- [docker cli](https://docs.docker.com/engine/install/) a containerization program.
- [docker compose](https://docs.docker.com/compose/install/standalone/) 
- Google OAuth App - Please see [guide](./oauth-guides/GOOGLE_OAUTH_GUIDE.md) on how to configure your OAuth app correctly for this application.
<!-- - Spotify OAuth App - Please see [guide](https://developer.spotify.com/documentation/web-api/concepts/apps) on how to configure your OAuth app correctly for this application. -->

This guide has a how-to on how you can configure your OAuth apps but it is worth reading from the sources like [Googl](https://developers.google.com/identity/protocols/oauth2) and [Spotify](https://developer.spotify.com/documentation/web-api/concepts/authorization) to understand what they do, it's not the aim of this guide to explain what OAuth is.

## Local Developer (With Dockercompose)
Getting started with docker compose, is ideally for when you don't want to have .NET installed on your machine.
This is ideal for developers who don't care that much about contributing to the backend but want the local APIs.

Please make sure you've went through the [pre-requistes section](#pre-requsites-needed) before proceeding.  
This section assumes you have docker and docker compose installed in your development environment/machine.  


### Configuration
Before we can run the app just make sure you have the needed configuration. Please follow the steps below to get started.   

- In the `src/backend/CrossDSP.WEBAPI` create a file called `appsettings.Development.json`
- You can then copy the contents of the `appsettings.json` that you should find in this repo, in the time of writing it was like this:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "GoogleOAuth2Options": {
    "ClientId": "[YOUR_CLIENT_ID]",
    "ClientSecret": "[YOUR_CLIENT_SECRET]",
    "RedirectUri": "http://localhost:5080/auth/google-callback",
    "TokenEndpoint": "https://accounts.google.com/o/oauth2/v2/auth",
    "OAuth2Endpoint": "https://oauth2.googleapis.com/token",
    "YouTubeResourceEndpoint": "https://www.googleapis.com"
  }
}
```
- In the `GoogleOAuth2Options` section you will need to replace the `ClientId` and `ClientSecret` with the values of Google OAuth App. You should created an app by now based off of this: [guide](./oauth-guides/GOOGLE_OAUTH_GUIDE.md)

That should be it for the configuration, now running the application.

#### Docker Compose Run.

- To run the application's backend, you will need to go the directory `src/` 
- You should find a file called [docker-compose-dev.yml](/src/docker-compose-dev.yml).
- Open the terminal in this directory and run the command:
```bash
# build docker file with compose
# this will create images.
docker compose -f docker-compose-dev.yml build
```
- The command above will create some docker images, you can the command `docker images`, you should see a result like this:
```bash
# command to view created images
docker images

# result of command:
REPOSITORY    TAG       IMAGE ID       CREATED       SIZE
src-backend   latest    71ae876bf81f   4 days ago    223MB
```
- That should confirm the image has been created.
- To run the app just run the command the command below
```bash
docker compose -f docker-compose-dev.yml up
```
- Then navigate to the url http://localhost:5080/swagger, you should the Swagger Open API definition of the endpoints the app has.

>[!NOTE]
> To stop the docker containers created by docker compose.  
> Run command `docker stop $(docker ps -aq)`


# Using the Application (API).

To see how to use the application, please see the following guides, the guide will try and explain every endpoint we have on a use case basis.

- [Getting Google OAuth Token and Getting YouTube API data](./google-use-cases/GET_TOKEN_AND_QUERY_YT_API.md)