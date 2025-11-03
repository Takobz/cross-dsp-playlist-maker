namespace CrossDSP.Infrastructure.Authentication.Spotify.Options
{
    public class SpotifyOptions 
    {
        public const string Section = "SpotifyOptions";
        
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string TokenEndpoint { get; set; } = string.Empty;
        public string OAuth2Endpoint { get; set; } = string.Empty;
        public string SpotifyResourceEndpoint { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
    }
}