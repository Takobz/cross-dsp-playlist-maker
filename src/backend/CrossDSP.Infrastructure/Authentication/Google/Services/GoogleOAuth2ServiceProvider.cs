using System.Net;
using System.Text.Json;
using CrossDSP.Infrastructure.Authentication.Common.Models;
using CrossDSP.Infrastructure.Authentication.Google.Options;
using CrossDSP.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.Authentication.Google.Services
{
    public interface IGoogleOAuthServiceProvider
    {
        Task<AuthorizationCodeFlowRedirect> InitiateAuthorizationCodeFlow();
        Task<DSPAccessToken> GetGoogleAccessToken(string code);
    }

    public class GoogleOAuthServiceProvider : IGoogleOAuthServiceProvider
    {
        private readonly IOptionsMonitor<GoogleOAuth2ServiceProviderOptions> _oauth2Options;
        private readonly ILogger<GoogleOAuthServiceProvider> _logger;
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive  = true
        };

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
            var requestQuery = new Dictionary<string, string>
            {
                { "client_id", $"{optionValues.ClientId}" },
                { "redirect_uri", $"{optionValues.RedirectUri}" },
                { "response_type", $"{GoogleOAuth2Defaults.CodeResponseType}" },
                { "scope", $"{GoogleOAuth2Defaults.YouTubeScope}" }, //space saparated for multiple scopes
                { "access_type", $"{GoogleOAuth2Defaults.OfflineAccessType}" },
                { "state", $"{Guid.NewGuid()}" },
                { "prompt", $"{GoogleOAuth2Defaults.SelectAccountPrompt}"}
            };

            return new AuthorizationCodeFlowRedirect(
                $"{optionValues.TokenEndpoint}{requestQuery.GenerateQueryParameters()}"
            );
        }

        public async Task<DSPAccessToken> GetGoogleAccessToken(string code)
        {
            var optionValues = _oauth2Options.CurrentValue;
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", optionValues.ClientId },
                { "client_secret", optionValues.ClientSecret },
                { "redirect_uri", optionValues.RedirectUri },
                { "grant_type", GoogleOAuth2Defaults.AuthorizationCodeGrantType }
            });

            var response = await _httpClient.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = formContent
            });

            //TODO: don't be optimistic handle failure :).
            //NOT WORKING - Err I get back: Required parameter is missing: response_type
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<DSPAccessToken>(
                    await response.Content.ReadAsStringAsync(),
                    _jsonOptions
                )!;
            }

            //TODO: Throw a domain custom exception we can handle :)
            throw new Exception("Mmmm Food!");
        }
    }
}