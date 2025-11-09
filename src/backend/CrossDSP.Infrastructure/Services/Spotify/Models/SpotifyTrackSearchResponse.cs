using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyTrackSearchResponse
    {
        [JsonPropertyName("tracks")]
        public SpotifySearchResponse<SpotifyTrack> Tracks { get; init; } = new();
    }
}