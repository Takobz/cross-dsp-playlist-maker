using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Authentication.Google.Options;
using CrossDSP.Infrastructure.Authentication.Google.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossDSP.Infrastructure.ServiceDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleOAuth2(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var googleOAuthSection = configuration.GetSection(GoogleOAuth2ServiceProviderOptions.SectionName);
            services.Configure<GoogleOAuth2ServiceProviderOptions>(googleOAuthSection);
            services.AddHttpClient<IGoogleOAuthServiceProvider, GoogleOAuthServiceProvider>(client =>
            {
                var options = new GoogleOAuth2ServiceProviderOptions();
                googleOAuthSection.Bind(options);
                client.BaseAddress = new Uri(options.TokenEndpoint);
            })
            .ConfigurePrimaryHttpMessageHandler(serviceProvider => {
                return new HttpClientHandler {
                    AllowAutoRedirect = false
                };
            });

            return services;
        }
    }
}