using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyTrackSearchResponse
    {
        [JsonPropertyName("tracks")]
        public SpotifyItemsResponse<SpotifyTrack> Tracks { get; init; } = new();
    }
}