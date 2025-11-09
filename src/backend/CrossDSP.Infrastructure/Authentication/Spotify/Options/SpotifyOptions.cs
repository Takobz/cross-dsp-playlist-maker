namespace CrossDSP.Infrastructure.Authentication.Spotify.Options
{
    public class SpotifyOptions 
    {
        public const string SectionName = "SpotifyOAuth2Options";
        
        public string ClientId { get; set; } = string.Empty; 
        public string ClientSecret { get; set; } = string.Empty;
        public string SpotifyAuthServerEndpoint { get; set; } = string.Empty;
        public string SpotifyResourceEndpoint { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
    }
}