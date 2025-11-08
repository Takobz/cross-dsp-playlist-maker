using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyEntity 
    {
        [JsonPropertyName("id")] 
        public string EntityId { get; init; } = string.Empty;

        [JsonPropertyName("href")] 
        public string HRef { get; init; } = string.Empty;

        [JsonPropertyName("uri")] 
        public string SpotifyUri { get; init; } = string.Empty;

        [JsonPropertyName("type")]
        public string EntityType { get; init; } = string.Empty;
    };
}