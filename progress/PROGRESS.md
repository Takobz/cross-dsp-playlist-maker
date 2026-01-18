# Progress Tracker ReadME

Who needs a sprint board for their own personal project - nobody baby!!

## What I am doing Now

- Make token utils use local storage for persisting the token for x amount of time.
- Spinner on waiting for access token
- Deploy this to the Raspberry Pi

### Nicities

- Use HttpOnly cookie for storing access tokens in client code.
- Fix double windows opening bug

## What I have done

- Authenticate app against YouTube Data API.
- Use user access token to search YouTube Data API.
- Search Spotify with client credentials of app.
- Handle Spotify user authorization
- Get user details (id, profile) from spotify apis
- Get user playlist after retrieving id
- Get user endpoint to get id.
- Add Tracks to a user's spotify playlist
- Learn basics of NextJS for UI. Used [NextJS guide](https://nextjs.org/learn/dashboard-app)
- Add Card To Initiate Google to Spotify
- Return authorize url with a key to check if authorization is done.
- Add endpoint to poll for access token
  - Check possible cache bug
- Update UI to also get the key.
- Poll the backend with they key.
- DSPAccessTokenContext to keep track of access tokens

