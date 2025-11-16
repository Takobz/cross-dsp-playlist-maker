using System.Net.Http.Json;
using System.Text.Json;
using CrossDSP.Infrastructure.Helpers;
using CrossDSP.Infrastructure.Services.Common.Models;
using CrossDSP.Infrastructure.Services.Spotify.Models;

namespace CrossDSP.Infrastructure.Services.Spotify
{
    public interface ISpotifyPlaylistService
    {
        Task<ServiceResults<SpotifyPlaylist>> GetUserPlaylists(string userId);
        Task<ServiceResult<SpotifySnapshot>> AddItemsToPlaylist(string playlistId, IEnumerable<string> items);
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

        public async Task<ServiceResult<SpotifySnapshot>> AddItemsToPlaylist(
            string playlistId,
            IEnumerable<string> items
        )
        {
            var requestBody = new SpotifyAddItems(
                items.ToSpotifyTrackUris()
            );

            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"v1/playlists/{playlistId}/tracks", UriKind.Relative),
                Content = JsonContent.Create(requestBody)
            });

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<SpotifySnapshot>(
                    await response.Content.ReadAsStreamAsync()
                )!;

                return new ServiceResult<SpotifySnapshot>(
                    data
                );
            }

            return new ServiceResult<SpotifySnapshot>();
        }
    }
}