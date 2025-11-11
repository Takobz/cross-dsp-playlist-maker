using System.Security.Claims;
using System.Text.Encodings.Web;
using CrossDSP.Infrastructure.Authentication.Google;
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
        //ISpotifyUserClient spotifyUserClient,
        IHttpContextAccessor httpContextAccessor)
        : AuthenticationHandler<SpotifyOAuthSchemeOptions>(options, loggerFactory, urlEncoder, systemClock)
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var httpContext = httpContextAccessor.HttpContext ?? throw new Exception("HttpContext can't be null");

            var spotifyAccessToken = ExtractBearerTokenFromHeader(httpContext.Request.Headers);
            if (string.IsNullOrEmpty(spotifyAccessToken))
            {
                return AuthenticateResult.Fail("Access Token Missing");
            }

            // var userProfile = await spotifyUserClient.GetCurrentUserProfileResponse(spotifyAccessToken);
            // if (userProfile == null)
            // {
            //     return AuthenticateResult.Fail("Failed to get spotify user for provided access token");
            // }

            // var claimsIdentity = new ClaimsIdentity([
            //     new Claim(SpotifyOAuthDefaults.SpotifyAccessToken, spotifyAccessToken),
            //     new Claim(SpotifyAuthenticationCustomClaims.SpotifyUserEntityId, userProfile.EntityId),
            //     new Claim(ClaimTypes.Name, userProfile.DisplayName),
            //     new Claim(ClaimTypes.Email, userProfile.Email)
            // ]);

            var claimsIdentity = new ClaimsIdentity([
            ]);

            return AuthenticateResult.Success(new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity),
                SpotifyOAuthDefaults.AuthenticationScheme
            ));
        }

        private static string ExtractBearerTokenFromHeader(IHeaderDictionary requestHeader)
        {
            if (requestHeader.TryGetValue("Authorization", out var accessTokenWithBearerPrefixStringValue) &&
                !string.IsNullOrEmpty(accessTokenWithBearerPrefixStringValue)
            )
            {
                //remove prefix "Bearer "
                var accessTokenWithBearerPrefix = accessTokenWithBearerPrefixStringValue.First();
                return accessTokenWithBearerPrefix![7..];
            }

            return string.Empty;
        }
    }
}