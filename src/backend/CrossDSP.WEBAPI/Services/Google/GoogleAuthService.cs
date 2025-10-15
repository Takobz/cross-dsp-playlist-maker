using CrossDSP.Infrastructure.Authentication.Google.Services;
using CrossDSP.WEBAPI.DTOs.Responses;

namespace CrossDSP.WEBAPI.Services.Google
{
    /*
    * Not advisable to return DTOs from here, I will clean up for now IDC :)
    */
    public interface IGoogleAuthService
    {
        public Task<string> GetGoogleAuthorizeUrl();
        public Task<DSPAccessTokenResponse> GetAccessToken(string code);
    }

    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly ILogger<GoogleAuthService> _logger;
        private readonly IGoogleOAuthServiceProvider _googleOAuthServiceProvider;

        public GoogleAuthService(
            ILogger<GoogleAuthService> logger,
            IGoogleOAuthServiceProvider googleOAuthServiceProvider
        )
        {
            _googleOAuthServiceProvider = googleOAuthServiceProvider;
            _logger = logger;
        }

        public async Task<string> GetGoogleAuthorizeUrl()
        {
            var result = await _googleOAuthServiceProvider.InitiateAuthorizationCodeFlow();
            return result.AuthorizeRedirectUrl;
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