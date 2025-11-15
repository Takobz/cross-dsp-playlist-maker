using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyUser : SpotifyEntity
    {
        [JsonPropertyName("country")]
        public string Country { get; init; } = string.Empty;
        
        [JsonPropertyName("display_name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; init; } = string.Empty; 
    }
}