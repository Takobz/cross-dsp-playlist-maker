using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Authentication.Google.Options;
using CrossDSP.Infrastructure.Authentication.Google.Services;
using CrossDSP.Infrastructure.Authentication.Spotify.Options;
using CrossDSP.Infrastructure.Authentication.Spotify.Services;
using CrossDSP.Infrastructure.HttpClientInfra.DelegatingHandlers;
using CrossDSP.Infrastructure.Services.Google;
using CrossDSP.Infrastructure.Services.Spotify;
using Microsoft.AspNetCore.Authentication;
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

            services.AddTransient<YouTubeResourceDelegatingHandler>();
            services.AddHttpClient<IYouTubeResourceService, YouTubeResourceService>(client =>
            {
                client.BaseAddress = new Uri(options.YouTubeResourceEndpoint);
            })
            .AddHttpMessageHandler<YouTubeResourceDelegatingHandler>();

            return services;
        }

        public static void AddGoogleAuthentication(this AuthenticationBuilder authBuilder)
        {
            authBuilder.AddScheme<GoogleOAuthSchemeOptions, GoogleOAuth2Handler>(
                GoogleOAuth2Defaults.GoogleOAuth2AuthenticationScheme,
                options => { }
            );
        }

        public static IServiceCollection AddSpotifyServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var spotifyOptionSection = configuration.GetSection(SpotifyOptions.SectionName);
            var options = new SpotifyOptions();
            spotifyOptionSection.Bind(options);

            services.Configure<SpotifyOptions>(spotifyOptionSection);

            //we cache the basic access token for the API using memory cache.
            services.AddMemoryCache();
            services.AddTransient<SpotifyBasicAccessTokenHandler>();
            services.AddHttpClient<ISpotifySearchService, SpotifySearchService>(client =>
            {
                client.BaseAddress = new Uri($"{options.SpotifyResourceEndpoint}/search");
            })
            .AddHttpMessageHandler<SpotifyBasicAccessTokenHandler>();

            services.AddHttpClient<ISpotifyOAuthProvider, SpotifyOAuthProvider>(client =>
            {
                client.BaseAddress = new Uri($"{options.SpotifyAuthServerEndpoint}");
            });

            return services;
        }
    }
}