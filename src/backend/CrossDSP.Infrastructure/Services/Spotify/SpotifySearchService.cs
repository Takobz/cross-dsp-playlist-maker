using CrossDSP.Infrastructure.Services.Spotify.Models;

namespace CrossDSP.Infrastructure.Services.Spotify
{
    public interface ISpotifySearchService { }

    public class SpotifySearchService : ISpotifySearchService
    {
        private readonly HttpClient _httpClient;

        public SpotifySearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SpotifySearchResponse<SpotifyTrack>> SearchTrackByName(
            string name,
            string? artistName = ""
        )
        {
            

            return new();
        }
    }
}