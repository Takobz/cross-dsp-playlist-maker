using System.Text.Json;
using CrossDSP.Infrastructure.Authentication.Common.Helpers;
using CrossDSP.Infrastructure.Authentication.Common.Models;
using CrossDSP.Infrastructure.Authentication.Spotify;
using CrossDSP.Infrastructure.Authentication.Spotify.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.HttpClientInfra.DelegatingHandlers
{
    public class SpotifyBasicAccessTokenHandler : DelegatingHandler
    {
        private readonly IOptions<SpotifyOptions> _options;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;

        private const string _spotifyAccessToken = SpotifyOAuthDefaults.SpotifyBasicAccessTokenCacheKey;

        public SpotifyBasicAccessTokenHandler(
            IOptions<SpotifyOptions> options,
            HttpClient httpClient,
            IMemoryCache memoryCache)
        {
            _options = options;
            _memoryCache = memoryCache;
            _httpClient = httpClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(_spotifyAccessToken, out DSPAccessToken? accessToken))
            {
                var base64ClientCredentials = CredentialsHelper.GenerateBase64ClientCredentials(
                    username: _options.Value.ClientId,
                    password: _options.Value.ClientSecret
                );

                var accessTokenRequestParams = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                });

                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {base64ClientCredentials}");

                var response = await _httpClient.SendAsync(new HttpRequestMessage
                {
                    Content = accessTokenRequestParams,
                    RequestUri = new Uri($"{_options.Value.TokenEndpoint}/api/token"),
                    Method = HttpMethod.Post
                });

                if (response.IsSuccessStatusCode)
                {
                    accessToken = await JsonSerializer.DeserializeAsync<DSPAccessToken>(
                        await response.Content.ReadAsStreamAsync(cancellationToken),
                        cancellationToken: cancellationToken
                    );

                    _memoryCache.Set(_spotifyAccessToken, accessToken);
                }
                else throw new Exception("Failed to get access token from spotify api.");
            }

            request.Headers.Remove("Authorization");
            request.Headers.Add("Authorization", $"Bearer {accessToken!.AccessToken}");

            return await base.SendAsync(request, cancellationToken);
        } 
    }
}