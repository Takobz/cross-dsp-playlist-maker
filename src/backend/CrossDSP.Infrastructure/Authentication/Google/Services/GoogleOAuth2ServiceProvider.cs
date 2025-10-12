using System.Net;
using System.Text.Json;
using CrossDSP.Infrastructure.Authentication.Common.Models;
using CrossDSP.Infrastructure.Authentication.Google.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.Authentication.Google.Services
{
    public interface IGoogleOAuthServiceProvider
    {
        Task<AuthorizationCodeFlowRedirect> InitiateAuthorizationCodeFlow();
    }

    public class GoogleOAuthServiceProvider : IGoogleOAuthServiceProvider
    {
        private readonly IOptionsMonitor<GoogleOAuth2ServiceProviderOptions> _oauth2Options;
        private readonly ILogger<GoogleOAuthServiceProvider> _logger;
        private readonly HttpClient _httpClient;

        public GoogleOAuthServiceProvider(
            IOptionsMonitor<GoogleOAuth2ServiceProviderOptions> options,
            ILogger<GoogleOAuthServiceProvider> logger,
            HttpClient httpClient
        )
        {
            _oauth2Options = options;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AuthorizationCodeFlowRedirect> InitiateAuthorizationCodeFlow()
        {
            var optionValues = _oauth2Options.CurrentValue;
            var requestQuery = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", $"{optionValues.ClientId}" },
                { "redirect_uri", $"{optionValues.RedirectUri}" },
                { "response_type", $"{GoogleOAuth2Defaults.CodeResponseType}" },
                { "scope", $"{GoogleOAuth2Defaults.YouTubeScope}" }, //space saparated for multiple scopes
                { "access_type", $"{GoogleOAuth2Defaults.OnlineAccessType}" },
                { "state", $"{Guid.NewGuid()}" },
                { "prompt", $"{GoogleOAuth2Defaults.SelectAccountPrompt}"}
            });

            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = requestQuery
            });

            if (response.StatusCode == HttpStatusCode.RedirectMethod ||
                response.StatusCode == HttpStatusCode.Redirect)
            {
                var authorizeUrl = response?.Headers?.Location?.AbsoluteUri ?? string.Empty;
                return new AuthorizationCodeFlowRedirect(
                    authorizeUrl
                );
            }

            //TODO: add domain exception here that will be handled by exception middleware component
            return new AuthorizationCodeFlowRedirect(
                string.Empty
            );
        }
    }
}