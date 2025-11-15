using System.Text.Json;
using CrossDSP.Infrastructure.Services.Common.Models;
using CrossDSP.Infrastructure.Services.Spotify.Models;

namespace CrossDSP.Infrastructure.Services.Spotify
{
    public interface ISpotifyPlaylistService
    {
        Task<ServiceResults<SpotifyPlaylist>> GetUserPlaylists(string userId);
    }

    public class SpotifyPlaylistService : ISpotifyPlaylistService
    {
        private readonly HttpClient _httpClient;

        public SpotifyPlaylistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResults<SpotifyPlaylist>> GetUserPlaylists(string userId)
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"v1/users/{userId}/playlists", UriKind.Relative)
            });

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<SpotifyItemsResponse<SpotifyPlaylist>>(
                    await response.Content.ReadAsStreamAsync()
                )!;

                return new ServiceResults<SpotifyPlaylist>(
                    data.Items
                );
            }

            return new ServiceResults<SpotifyPlaylist>();
        }
    }
}