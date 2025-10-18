using System.Text.Json;
using CrossDSP.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace CrossDSP.Infrastructure.Services.Google
{
    public interface IYouTubeResourceService
    {
        Task SearchYouTube(string query);
    }

    public class YouTubeResourceService : IYouTubeResourceService
    {
        private readonly ILogger<YouTubeResourceService> _logger;
        private readonly HttpClient _httpClient;

        public YouTubeResourceService(
            ILogger<YouTubeResourceService> logger,
            HttpClient httpClient
        )
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task SearchYouTube(string query)
        {
            var searchQueryParams = new Dictionary<string, string>
            {
                { "part", "snippet" },
                { "q", query }
            };

            var x = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    "/youtube/v3/search" + $"{searchQueryParams.GenerateQueryParameters()}",
                    UriKind.Relative
                )
            });

            var y = await x.Content.ReadAsStringAsync();
        }
    }
}