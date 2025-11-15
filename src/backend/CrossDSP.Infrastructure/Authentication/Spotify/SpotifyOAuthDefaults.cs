namespace CrossDSP.Infrastructure.Authentication.Spotify
{
    public static class SpotifyOAuthDefaults
    {
        #region Authentication Handler Constants
        public const string AuthenticationScheme = "SpotifyAuthentication";
        public const string SpotifyAccessTokenClaimKey = "SpotifyAccessToken";
        public const string SpotifyUserEntityIdClaimKey = "SpotifyUserEntityId";
        public const string SpotifyUserPolicy = "SpotifyUserPolicy";
        #endregion

        #region Application Specific Auth Constants
        public const string SpotifyBasicAccessTokenCacheKey = "spotify_app_basic_access_token";
        #endregion

        #region Spotify Authentication Server Constants
        public const string PlayListModifyPublic = "playlist-modify-public";
        public const string UserLibraryRead = "user-library-read";
        public const string UserReadEmail = "user-read-email";

        public static string UserPublicPlaylistSpotifyScopes() =>
            $"{PlayListModifyPublic} {UserLibraryRead} {UserReadEmail}";

        #endregion

        
    }
}