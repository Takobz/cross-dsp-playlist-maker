using System.Net;
using CrossDSP.Infrastructure.Authentication.Spotify.Options;
using CrossDSP.Infrastructure.Helpers;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.Authentication.Spotify.Services
{
    public interface ISpotifyOAuthProvider
    {
        public Task<string> InitiateAuthorizationRequest(string scopes);
    }

    public class SpotifyOAuthProvider : ISpotifyOAuthProvider
    {
        private readonly SpotifyOptions _spotifyOptions;
        private readonly HttpClient _httpClient;

        public SpotifyOAuthProvider(
            IOptions<SpotifyOptions> options,
            HttpClient httpClient)
        {
            _spotifyOptions = options.Value;
            _httpClient = httpClient;
        }

        public async Task<string> InitiateAuthorizationRequest(string scopes)
        {
            var requestQuery = new Dictionary<string, string>
            {
                { "client_id", _spotifyOptions.ClientId },
                { "response_type", "code" },
                { "redirect_uri", _spotifyOptions.RedirectUri },
                { "scopIe", scopes },
                { "state", $"{Guid.NewGuid()}" },
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
                var spotifyAuthorizeUrl = response?.Headers?.Location?.AbsoluteUri ?? string.Empty;
                return spotifyAuthorizeUrl;
            }

            throw new Exception("Failed To Initiate authorize request");
        }
    }
}