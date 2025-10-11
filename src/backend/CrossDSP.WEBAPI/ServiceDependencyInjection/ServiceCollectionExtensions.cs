using CrossDSP.WEBAPI.Services.Google;

namespace CrossDSP.WEBAPI.ServiceDependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services
        )
        {
            services.AddTransient<IGoogleAuthService, GoogleAuthService>();
            return services;
        }
    }
}