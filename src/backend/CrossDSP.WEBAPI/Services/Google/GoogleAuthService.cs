using CrossDSP.Infrastructure.Authentication.Google.Services;

namespace CrossDSP.WEBAPI.Services.Google
{
    public interface IGoogleAuthService
    {
        public Task<string> GetGoogleAuthorizeUrl();
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
    }
}