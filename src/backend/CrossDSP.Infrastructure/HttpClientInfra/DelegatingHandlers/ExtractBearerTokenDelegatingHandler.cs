using CrossDSP.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;

namespace CrossDSP.Infrastructure.HttpClientInfra.DelegatingHandlers
{
    /// <summary>
    /// Extracts bearer token from Request and reuses it.
    /// Use this when the passed bearer can be resued by calling services.
    /// </summary>
    public class ExtractingBearerTokenDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExtractingBearerTokenDelegatingHandler(
            IHttpContextAccessor httpContextAccessor
        )
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken
        )
        {
            var httpContext = _httpContextAccessor.HttpContext ?? throw new Exception("HttpContext can't be null");
            var bearerToken = httpContext.Request.Headers.ExtractBearerTokenFromHeader();
            if (string.IsNullOrEmpty(bearerToken))
            {
                throw new Exception("Bearer Token is required to continue");
            }

            if (request.Headers.TryGetValues("Authorization", out _))
            {
                request.Headers.Remove("Authorization"); 
            }

            request.Headers.Add("Authorization", $"Bearer {bearerToken}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}