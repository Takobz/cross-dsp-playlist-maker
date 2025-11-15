using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Authentication.Google.Options;
using CrossDSP.Infrastructure.Authentication.Google.Services;
using CrossDSP.Infrastructure.Authentication.Spotify;
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
        public static IServiceCollection AddInfrastructureDependencies(
            this IServiceCollection services
        )
        {
            services.AddTransient<YouTubeResourceDelegatingHandler>();
            services.AddTransient<SpotifyBasicAccessTokenHandler>();
            services.AddTransient<LoggingDelegatingHandler>();
            services.AddTransient<ExtractingBearerTokenDelegatingHandler>();

            //needed by spotify for cashing basic token
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            return services;
        }

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
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>();

            services.AddHttpClient<IYouTubeResourceService, YouTubeResourceService>(client =>
            {
                client.BaseAddress = new Uri(options.YouTubeResourceEndpoint);
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddHttpMessageHandler<YouTubeResourceDelegatingHandler>();

            return services;
        }

        public static AuthenticationBuilder AddGoogleAuthentication(this AuthenticationBuilder authBuilder)
        {
            authBuilder.AddScheme<GoogleOAuthSchemeOptions, GoogleOAuth2Handler>(
                GoogleOAuth2Defaults.GoogleOAuth2AuthenticationScheme,
                options => { }
            );

            return authBuilder;
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
            
            services.AddHttpClient<ISpotifySearchService, SpotifySearchService>(client =>
            {
                client.BaseAddress = new Uri($"{options.SpotifyResourceEndpoint}/search");
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddHttpMessageHandler<SpotifyBasicAccessTokenHandler>();

            services.AddHttpClient<ISpotifyUserService, SpotifyUserService>(client =>
            {
                client.BaseAddress = new Uri($"{options.SpotifyResourceEndpoint}/me");
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddHttpMessageHandler<ExtractingBearerTokenDelegatingHandler>();

            services.AddHttpClient<ISpotifyPlaylistService, SpotifyPlaylistService>(client =>
            {
               client.BaseAddress = new Uri($"{options.SpotifyResourceEndpoint}"); 
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddHttpMessageHandler<ExtractingBearerTokenDelegatingHandler>();

            services.AddHttpClient<ISpotifyOAuthProvider, SpotifyOAuthProvider>(client =>
            {
                client.BaseAddress = new Uri($"{options.SpotifyAuthServerEndpoint}");
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .ConfigurePrimaryHttpMessageHandler(serviceProvider =>
            {
                return new HttpClientHandler
                {
                    AllowAutoRedirect = false
                };
            });

            return services;
        }

        public static AuthenticationBuilder AddSpotifyAuthentication(this AuthenticationBuilder authBuilder)
        {
            authBuilder.AddScheme<SpotifyOAuthSchemeOptions, SpotifyAuthenticationHandler>(
                SpotifyOAuthDefaults.AuthenticationScheme,
                options => { }
            );

            return authBuilder;
        }
    }
}