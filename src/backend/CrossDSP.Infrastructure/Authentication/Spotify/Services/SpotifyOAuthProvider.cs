using System.Net;
using System.Text.Json;
using CrossDSP.Infrastructure.Authentication.Common.Helpers;
using CrossDSP.Infrastructure.Authentication.Common.Models;
using CrossDSP.Infrastructure.Authentication.Spotify.Options;
using CrossDSP.Infrastructure.Extensions;
using CrossDSP.Infrastructure.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.Authentication.Spotify.Services
{
    public interface ISpotifyOAuthProvider
    {
        public Task<AuthorizationCodeFlowRedirect> InitiateAuthorizationRequest(string scopes);
        public Task<DSPAccessToken> GetUserAccessToken(string code, string state);
        public Task<DSPAccessToken?> TryGetAccessTokenFromCache(string authorizationState);
    }

    public class SpotifyOAuthProvider : ISpotifyOAuthProvider
    {
        private readonly SpotifyOptions _spotifyOptions;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly int _minutesInSeconds = 500;

        public SpotifyOAuthProvider(
            IOptions<SpotifyOptions> options,
            HttpClient httpClient,
            IMemoryCache memoryCache
        )
        {
            _spotifyOptions = options.Value;
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<AuthorizationCodeFlowRedirect> InitiateAuthorizationRequest(string scopes)
        {
            var state = $"{Guid.NewGuid()}";
            var requestQuery = new Dictionary<string, string>
            {
                { "client_id", _spotifyOptions.ClientId },
                { "response_type", "code" },
                { "redirect_uri", _spotifyOptions.RedirectUri },
                { "scope", scopes },
                { "state", state },
                { "show_dialog", $"{true}" },
            };

            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                RequestUri = new Uri(
                    $"authorize{requestQuery.GenerateQueryParameters()}",
                    UriKind.Relative
                ),
                Method = HttpMethod.Get
            });

            if (response.StatusCode == HttpStatusCode.RedirectMethod ||
                response.StatusCode == HttpStatusCode.Redirect)
            {
                /*
                * Initially set access token as null.
                */
                await _memoryCache.GetOrSetItemAsync(
                    key: state,
                    func: async () => await Task.FromResult((DSPAccessToken)null!),
                    expiryTimeInSeconds: _minutesInSeconds
                );

                var spotifyAuthorizeUrl = response?.Headers?.Location?.AbsoluteUri ?? string.Empty;
                return new AuthorizationCodeFlowRedirect(
                    spotifyAuthorizeUrl,
                    state
                );
            }

            throw new Exception("Failed To Initiate authorize request");
        }

        public async Task<DSPAccessToken> GetUserAccessToken(
            string code,
            string state
        )
        {
            var result = await _memoryCache.GetOrSetItemAsync(
                key: state,
                func: async () =>
                {
                    var base64ClientCredentials = CredentialsHelper.GenerateBase64ClientCredentials(
                    _spotifyOptions.ClientId,
                    _spotifyOptions.ClientSecret
                    );

                    var request = new FormUrlEncodedContent(new Dictionary<string, string> 
                    {
                        { "grant_type", "authorization_code" },
                        { "code", code },
                        { "redirect_uri", _spotifyOptions.RedirectUri }
                    });

                    _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {base64ClientCredentials}");
                    var response = await _httpClient.SendAsync(new HttpRequestMessage 
                    {
                        RequestUri = new Uri("/api/token", UriKind.Relative),
                        Method = HttpMethod.Post,
                        Content = request
                    });

                    if (response.IsSuccessStatusCode) 
                    {
                        return JsonSerializer.Deserialize<DSPAccessToken>(
                            await response.Content.ReadAsStreamAsync()
                        )!;
                    }

                    throw new Exception($"Failed to get access token for authorization code: {code}");
                },
                expiryTimeInSeconds: _minutesInSeconds
            );

            return result!;
        }

        public async Task<DSPAccessToken?> TryGetAccessTokenFromCache(string authorizationState)
        {
            return await Task.FromResult(
                _memoryCache.TryGetItem<DSPAccessToken>(
                    key: authorizationState
                )
            );
        }
    }
}