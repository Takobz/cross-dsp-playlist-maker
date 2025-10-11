using System.ComponentModel.DataAnnotations;

namespace CrossDSP.Infrastructure.Authentication.Google.Options
{
    public class GoogleOAuth2ServiceProviderOptions
    {
        public const string SectionName = "GoogleOAuth2Options";

        [Required(ErrorMessage = $"ClientId is need for options: {SectionName}")]
        public string ClientId { get; set; } = string.Empty;

        [Required(ErrorMessage = $"ClientSecret is need for options: {SectionName}")]
        public string ClientSecret { get; set; } = string.Empty;

        [Required(ErrorMessage = $"RedirectUri is need for options: {SectionName}")]
        public string RedirectUri { get; set; } = string.Empty;

        [Required(ErrorMessage = $"TokenEndpoint is need for options: {SectionName}")]
        public string TokenEndpoint { get; set; } = string.Empty;
    }
}