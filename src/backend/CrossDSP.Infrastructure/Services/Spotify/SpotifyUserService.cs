using System.Text.Json;
using CrossDSP.Infrastructure.Services.Common.Models;
using CrossDSP.Infrastructure.Services.Spotify.Models;

namespace CrossDSP.Infrastructure.Services.Spotify
{
    public interface ISpotifyUserService
    {
        public Task<ServiceResult<SpotifyUser>> GetUser();
    }

    public class SpotifyUserService : ISpotifyUserService
    {
        private readonly HttpClient _httpClient;

        public SpotifyUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<SpotifyUser>> GetUser()
        {
            var response = await _httpClient.SendAsync(
                new HttpRequestMessage
                {
                    Method = HttpMethod.Get
                }
            );

            if (response.IsSuccessStatusCode)
            {
                var data =  JsonSerializer.Deserialize<SpotifyUser>(
                    await response.Content.ReadAsStreamAsync()
                )!;

                return new ServiceResult<SpotifyUser>(
                    data
                );
            }

            return new ServiceResult<SpotifyUser>();
        }
    }
}