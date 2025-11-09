using System.Text.Json;
using CrossDSP.Infrastructure.Helpers;
using CrossDSP.Infrastructure.Services.Spotify.Models;

namespace CrossDSP.Infrastructure.Services.Spotify
{
    public interface ISpotifySearchService
    {
        public Task<SpotifyTrackSearchResponse> SearchTrackByName(string trackName, string? artistName = "");
    }

    public class SpotifySearchService : ISpotifySearchService
    {
        private readonly HttpClient _httpClient;

        public SpotifySearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SpotifyTrackSearchResponse> SearchTrackByName(
            string trackName,
            string? artistName = ""
        )
        {
            var query = string.IsNullOrEmpty(artistName) ?
                trackName.BuildSpotifySearchQuery() :
                trackName.BuildSpotifySearchQuery(new Dictionary<string, string>
                {
                    { "artist", artistName }
                }
            );

            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(query, UriKind.Relative)
            });

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<SpotifyTrackSearchResponse>(
                    await response.Content.ReadAsStringAsync()
                )!;
            }

            //TODO: have a common way to handle spotify responses.
            throw new Exception("Faile to Search the Spotify APIs");
        }
    }
}