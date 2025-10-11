using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrossDSP.Infrastructure.Authentication.Google
{
    public class GoogleOAuth2Handler : AuthenticationHandler<GoogleOAuthSchemeOptions>
    {
        public GoogleOAuth2Handler(
            IOptionsMonitor<GoogleOAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            

            throw new NotImplementedException();
        }
    }
}