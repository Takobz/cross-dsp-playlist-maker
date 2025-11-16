using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifySnapshot
    {
        [JsonPropertyName("snapshot_id")]
        public string SnapshotId { get; init; } = string.Empty;
    }
}