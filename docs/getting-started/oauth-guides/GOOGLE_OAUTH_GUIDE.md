# GOOGLE OAuth Guide.
In this guide we will look at how to create an OAuth App that will allow us to authenticate against google.


## Creating OAuth App In Google Console App
The first thing we need to do is create an oauth app in the google console app. Please follow the steps listed below:

- Login to the [google console](https://console.cloud.google.com/) using your google account.

- After login select a project you'd love to work in, if you have none you create one or use the already created `No Organisation`:
[create-or-select-google-project](../../assets/create-or-select-google-project.png)

- After the project is created you'll be navigated to your project, on the search bar search for `oauth`, one of the results show be `Credentials`, click on it:  
[google-credentials-search-result](../../assets/google-credentials-search-result.png)

- On the credentials page find the `Create Credentials` button and click on it and select `OAuth client ID`, as seen below:  
[google-create-credentials-button](../../assets/google-create-credentials-button.png)

- If you haven't configured the consent screen, you a warning just click `Configure Consent Screen`:  
[google-configure-consent-screen-warn](../../assets/google-configure-consent-screen-warn.png)

- On the consent screen just fill in basic information like the application name. Just make sure the Audience is `External` because we want authenticate anyone who has google account with this app:
[google-consent-screen-details](../../assets/google-consent-screen-details.png)

- After the consent is created, go to `client` menu option and create client, as seen below:  
[google-create-client-after-consent](../../assets/google-create-client-after-consent.png)

- When creating client make sure the values are as follows below, you can always change the name to whatever you want just make sure the application type is `web application`, also the redirect can change as the app evolves so just make sure it is consistent with what is the `appsettings.json` of the backend code:  
[google-oauth-client-form-values](../../assets/google-oauth-client-form-values.png)

- When click create, you be present with a `Client ID` and `Client Secret`. You can download as JSON or copy, just keep them safe, you can only view the secret once, so don't lose it:  
[google-oauth-client-id-and-secret](../../assets/google-oauth-client-id-and-secret.png)

Now that the app is created, and since it is a testing, not yet published for public use we'd need to manually add users who can authenticate against it, most likey yourself. Seen next section.

## Add Yourself As Test User

- Go to the `Auidence` menu and you should see a `Test Users` option add a user like shown below:
[google-oauth-add-test-user](../../assets/google-oauth-add-test-user.png)

- After the user is added you should see them listed on the below the Test User option. That's it you are done.

After creating the app and adding yourself as test user we need to tell Google to allow the app to be used to query the YouTube Data API, since that's the API we will be using to get YouTube Music data.

## Enabling App To Use YouTube APIs and OIDC

- On the search in console search for `YouTube Data API`, click on the YouTube Data API V3 (it was V3 at the time of writing):  
[google-yt-data-api-search](../../assets/google-yt-data-api-search.png)

- You should see an `Enable` button, click on it:
[google-enable-yt-data-api](../../assets/google-enable-yt-data-api.png)

- After you've enabled you should see the app you created on the `Credentials` drop, as seen below:
[google-enabled-yt-data-api-app](../../assets/google-enabled-yt-data-api-app)

