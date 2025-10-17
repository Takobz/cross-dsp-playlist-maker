using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Authentication.Google.Options;
using CrossDSP.Infrastructure.Authentication.Google.Services;
using CrossDSP.Infrastructure.HttpClientInfra.DelegatingHandlers;
using CrossDSP.Infrastructure.Services.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossDSP.Infrastructure.ServiceDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGoogleServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var googleOAuthSection = configuration.GetSection(GoogleOAuth2ServiceProviderOptions.SectionName);
            var options = new GoogleOAuth2ServiceProviderOptions();
            googleOAuthSection.Bind(options);

            services.Configure<GoogleOAuth2ServiceProviderOptions>(googleOAuthSection);
            services.AddHttpClient<IGoogleOAuthServiceProvider, GoogleOAuthServiceProvider>(client =>
            {
                client.BaseAddress = new Uri(options.OAuth2Endpoint);
            });

            services.AddHttpClient<IYouTubeResourceService, YouTubeResourceService>(client =>
            {
                client.BaseAddress = new Uri(options.YouTubeResourceEndpoint);
            })
            .AddHttpMessageHandler<YouTubeResourceDelegatingHandler>();

            return services;
        }
    }
}