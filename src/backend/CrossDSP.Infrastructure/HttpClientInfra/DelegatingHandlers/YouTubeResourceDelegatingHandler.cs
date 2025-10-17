using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;

namespace CrossDSP.Infrastructure.HttpClientInfra.DelegatingHandlers
{
    public class YouTubeResourceDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public YouTubeResourceDelegatingHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var googleAccessToken = _accessor.HttpContext.User
                .GetClaimValueByName(GoogleOAuth2Defaults.GoogleOAuth2AccessTokenClaim);

            request.Headers.Remove("Authorization");
            request.Headers.Add("Authorization", $"Bearer {googleAccessToken}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}