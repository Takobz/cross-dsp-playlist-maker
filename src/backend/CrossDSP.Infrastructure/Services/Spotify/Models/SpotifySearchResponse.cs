using System.Text.Json.Serialization;

namespace CrossDSP.Infrastructure.Services.Spotify.Models
{
    /// <summary>
    /// Wrapper class that has a list of Spotify items.
    /// </summary>
    /// <typeparam name="T">Type of items that will be returned spotify</typeparam>
    public record SpotifyItemsResponse<T> where T : SpotifyEntity
    {
        [JsonPropertyName("href")]
        public string HRef { get; init; } = string.Empty;

        [JsonPropertyName("limit")]
        public int Limit { get; init; }

        [JsonPropertyName("next")]
        public string Next { get; init; } = string.Empty;

        [JsonPropertyName("previous")]
        public string Previous { get; init; } = string.Empty;

        [JsonPropertyName("offset")]
        public int Offset { get; init; }

        [JsonPropertyName("total")] 
        public int Total { get; init; }

        [JsonPropertyName("items")]
        public IEnumerable<T> Items { get; init; } = [];
    } 
}