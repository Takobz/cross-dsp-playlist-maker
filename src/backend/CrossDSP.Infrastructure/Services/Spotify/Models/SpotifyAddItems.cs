using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyAddItems
    {
        public SpotifyAddItems(
            IEnumerable<string> uris,
            int? position = default
        )
        {
            Uris = uris;
            Position = position;
        }

        [JsonPropertyName("uris")]
        public IEnumerable<string> Uris { get; init; }

        [JsonPropertyName("position")]
        public int? Position { get; init; }
    }
}