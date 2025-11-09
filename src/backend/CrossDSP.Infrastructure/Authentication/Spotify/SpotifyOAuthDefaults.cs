namespace CrossDSP.Infrastructure.Authentication.Spotify
{
    public static class SpotifyOAuthDefaults
    {
        public const string SpotifyBasicAccessTokenCacheKey = "spotify_app_basic_access_token";

        public const string PlayListModifyPublic = "playlist-modify-public";
        public const string UserLibraryRead = "user-library-read";
        public const string UserReadEmail = "user-read-email";

        public static string CreateFavoritesPlaylistSpotifyScopes() =>
            $"{PlayListModifyPublic} {UserLibraryRead} {UserReadEmail}";
    }
}