using Microsoft.Extensions.Logging;

namespace CrossDSP.Infrastructure.Services.Google
{
    public interface IYouTubeResourceService { }

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
            var x = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                
            });
        }
    }
}