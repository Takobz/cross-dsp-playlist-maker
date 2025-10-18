using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CrossDSP.Infrastructure.Helpers;
using System.Security.Claims;

namespace CrossDSP.Infrastructure.Authentication.Google
{
    public class GoogleOAuth2Handler : AuthenticationHandler<GoogleOAuthSchemeOptions>
    {
        private readonly IHttpContextAccessor _accessor;

        public GoogleOAuth2Handler(
            IOptionsMonitor<GoogleOAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor accessor
        ) : base(options, logger, encoder, clock)
        {
            _accessor = accessor;
        }

        /*
        This auth handler doesn't really validate the bearer token
        It adds it as a claim to the Google identity 
        The underlying services will propergate the 401 if the access token has expired.
        */
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var accessToken = _accessor.HttpContext.Request.Headers.ExtractBearerTokenFromHeader();
            if (string.IsNullOrEmpty(accessToken))
            {
                await Task.FromResult(AuthenticateResult.Fail("Google Access Token Required"));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims: [
                    new Claim(GoogleOAuth2Defaults.GoogleOAuth2AccessTokenClaim, accessToken)
                ],
                authenticationType: GoogleOAuth2Defaults.GoogleOAuth2Identity
            );

            return AuthenticateResult.Success(new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity),
                GoogleOAuth2Defaults.GoogleOAuth2AuthenticationScheme
            ));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            //TODO: Handle 401
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized baby"));
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            var user = _accessor.HttpContext.User;
            await base.HandleForbiddenAsync(properties);
        }
    }
}