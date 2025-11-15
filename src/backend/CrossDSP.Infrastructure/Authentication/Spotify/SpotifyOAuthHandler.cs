using System.Security.Claims;
using System.Text.Encodings.Web;
using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Helpers;
using CrossDSP.Infrastructure.Services.Spotify;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.Authentication.Spotify
{
    public class SpotifyAuthenticationHandler(
        IOptionsMonitor<SpotifyOAuthSchemeOptions> options,
        ILoggerFactory loggerFactory,
        UrlEncoder urlEncoder,
        ISystemClock systemClock,
        ISpotifyUserService spotifyUserClient,
        IHttpContextAccessor httpContextAccessor)
        : AuthenticationHandler<SpotifyOAuthSchemeOptions>(options, loggerFactory, urlEncoder, systemClock)
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var userProfileResult = await spotifyUserClient.GetUser();
            if (!userProfileResult.HasData)
            {
                return AuthenticateResult.Fail("Failed to get spotify user for provided access token");
            }

            var httpContext = httpContextAccessor.HttpContext;
            var bearerToken = httpContext.Request.Headers.ExtractBearerTokenFromHeader();

            var userProfile = userProfileResult.Data!;
            var claimsIdentity = new ClaimsIdentity([
                new Claim(SpotifyOAuthDefaults.SpotifyAccessTokenClaimKey, bearerToken),
                new Claim(SpotifyOAuthDefaults.SpotifyUserEntityIdClaimKey, userProfile.EntityId),
                new Claim(ClaimTypes.Name, userProfile.Name),
                new Claim(ClaimTypes.Email, userProfile.Email)
            ]);

            return AuthenticateResult.Success(new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity),
                SpotifyOAuthDefaults.AuthenticationScheme
            ));
        }
    }
}