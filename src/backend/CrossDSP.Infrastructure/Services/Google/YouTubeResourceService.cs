using System.Text.Json;
using CrossDSP.Infrastructure.Helpers;
using CrossDSP.Infrastructure.Services.Google.Models;
using Microsoft.Extensions.Logging;

namespace CrossDSP.Infrastructure.Services.Google
{
    public interface IYouTubeResourceService
    {
        Task<YouTubeSearchResult> SearchYouTube(string query);
    }

    public class YouTubeResourceService : IYouTubeResourceService
    {
        private readonly ILogger<YouTubeResourceService> _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        //please see: https://developers.google.com/youtube/v3/docs/videoCategories
        private readonly string _youTubeVideosMusicCategory = "10";

        public YouTubeResourceService(
            ILogger<YouTubeResourceService> logger,
            HttpClient httpClient
        )
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<YouTubeSearchResult> SearchYouTube(string query)
        {
            var searchQueryParams = new Dictionary<string, string>
            {
                { "part", "snippet" },
                { "q", query },
                { "type", "video" },
                { "videoCategoryId", _youTubeVideosMusicCategory }
            };

            var result = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    "/youtube/v3/search" + $"{searchQueryParams.GenerateQueryParameters()}",
                    UriKind.Relative
                )
            });

            /*
             TODO build handler / common util to handle YT errors
             https://developers.google.com/youtube/v3/docs/search/list#errors
            */
            if (result.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<YouTubeSearchResult>(
                    await result.Content.ReadAsStringAsync(),
                    _jsonOptions
                )!;
            }

            return new();
        }
    }
}