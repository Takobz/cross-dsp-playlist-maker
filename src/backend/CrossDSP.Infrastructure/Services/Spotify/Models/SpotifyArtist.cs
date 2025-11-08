using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    public record SpotifyArtist : SpotifyEntity
    {
        [JsonPropertyName("name")]
        public string ArtistName { get; init; } = string.Empty; 
    }
}