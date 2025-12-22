using CrossDSP.Infrastructure.Authentication.Google.Services;
using CrossDSP.WEBAPI.DTOs.Responses;
using CrossDSP.WEBAPI.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace CrossDSP.WEBAPI.Services.Google
{
    /*
    * Not advisable to return DTOs from here, I will clean up for now IDC :)
    */
    public interface IGoogleAuthService
    {
        public Task<AuthorizeInitResponse> GetGoogleAuthorizeUrl();
        public Task<DSPAccessTokenResponse> GetAccessToken(string code);
    }

    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly ILogger<GoogleAuthService> _logger;
        private readonly IGoogleOAuthServiceProvider _googleOAuthServiceProvider;
        private readonly IMemoryCache _memoryCache;

        private readonly int _minutesInSeconds = 300;

        public GoogleAuthService(
            ILogger<GoogleAuthService> logger,
            IGoogleOAuthServiceProvider googleOAuthServiceProvider,
            IMemoryCache memoryCache
        )
        {
            _googleOAuthServiceProvider = googleOAuthServiceProvider;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<AuthorizeInitResponse> GetGoogleAuthorizeUrl()
        {
            var result = await _googleOAuthServiceProvider.InitiateAuthorizationCodeFlow();
            var authorizeInitResponse = new AuthorizeInitResponse(
                redirectUri: result.AuthorizeRedirectUrl,
                authorizationState: result.AuthorizationState
            );

            await _memoryCache.GetOrSetItemAsync(
                key: result.AuthorizationState,
                func: async () => await Task.FromResult(authorizeInitResponse),
                expiryTimeInSeconds: _minutesInSeconds
            );

            return authorizeInitResponse;
        }

        public async Task<DSPAccessTokenResponse> GetAccessToken(string code)
        {
            var result = await _googleOAuthServiceProvider.GetGoogleAccessToken(code);

            return new DSPAccessTokenResponse(
                accessToken: result.AccessToken,
                expiresIn: result.ExpiresIn,
                refreshToken: result.RefreshToken
            );
        }
    }
}