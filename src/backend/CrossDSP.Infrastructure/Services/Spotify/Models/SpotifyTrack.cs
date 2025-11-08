using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyTrack : SpotifyEntity
    {
        [JsonPropertyName("artists")]
        public IEnumerable<SpotifyArtist> Artists { get; init; } = [];

        [JsonPropertyName("name")]
        public string TrackName { get; init; } = string.Empty;
    }
}