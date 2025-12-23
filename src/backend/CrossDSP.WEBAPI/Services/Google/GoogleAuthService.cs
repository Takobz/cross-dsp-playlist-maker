using CrossDSP.Infrastructure.Authentication.Common.Models;
using CrossDSP.Infrastructure.Authentication.Google.Services;
using CrossDSP.Infrastructure.Extensions;
using CrossDSP.WEBAPI.DTOs.Responses;
using Microsoft.Extensions.Caching.Memory;

namespace CrossDSP.WEBAPI.Services.Google
{
    /*
    * Not advisable to return DTOs from here, I will clean up for now IDC :)
    */
    public interface IGoogleAuthService
    {
        /// <summary>
        /// Returns Google Auth API URL to authenticate google resource owner
        /// </summary>
        public Task<AuthorizeInitResponse> GetGoogleAuthorizeUrl();

        /// <summary>
        /// Swap authorization code for access token <br/>
        /// Adds access token to cache for later retrieval by UI/Client
        /// </summary>
        public Task<DSPAccessTokenResponse> GetAccessToken(string code, string state);

        /// <summary>
        /// Trys to get access token from cache <br/>
        /// </summary>
        public Task<DSPAccessTokenResponse?> TryGetAccessTokenFromCache(string authorizationState);
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
                func: async () => await Task.FromResult((DSPAccessTokenResponse)null!),
                expiryTimeInSeconds: _minutesInSeconds
            );

            return authorizeInitResponse;
        }

        public async Task<DSPAccessTokenResponse> GetAccessToken(
            string code,
            string state
        )
        {
            var result = await _memoryCache.GetOrSetItemAsync(
                key: state,
                func: async () => await _googleOAuthServiceProvider.GetGoogleAccessToken(code),
                expiryTimeInSeconds: _minutesInSeconds
            );

            return new DSPAccessTokenResponse(
                accessToken: result!.AccessToken,
                expiresIn: result.ExpiresIn,
                refreshToken: result.RefreshToken
            );
        }


        public async Task<DSPAccessTokenResponse?> TryGetAccessTokenFromCache(string authorizationState)
        {
            var result = await Task.FromResult(
                _memoryCache.TryGetItem<DSPAccessToken>(
                    key: authorizationState
                )
            );

            if (result == null) return default;

            return new DSPAccessTokenResponse(
                accessToken: result!.AccessToken,
                expiresIn: result.ExpiresIn,
                refreshToken: result.RefreshToken
            );
        }
    }
}