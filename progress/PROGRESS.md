# Progress Tracker ReadME

Who needs a sprint board for their own personal project - nobody baby!!

## What I am doing Now

- **Styling fixes LATER - get mvp up**
- Get user Spotify access token
- small form to create playlist
- add tracks to playlist
- Move function switches to functions util.
- Add track via Add Btn to Spotify
- Spinner on waiting for access token
- Deploy this to the Raspberry Pi
- Figure out tailwindcss mess with styling.

### Bugs

- local storage init instantiation null ref error. (Low Priority)

### Nicities

- Use HttpOnly cookie for storing access tokens in client code.

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
- Make token utils use local storage for persisting the token for x amount of time.
- Add expiry time to the cache basic token in the backend.
