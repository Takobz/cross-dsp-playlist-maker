namespace CrossDSP.Infrastructure.Authentication.Common.Models
{
    public record AuthorizationCodeFlowRedirect(
        string AuthorizeRedirectUrl
    );
}