using Radiao.Domain.Options;

namespace Radiao.Api.Configurations
{
    public static class OptionsConfig
    {
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleAuthenticationOptions>(options
                => configuration
                    .GetSection("Authentication")
                    .GetSection("Google")
                    .Bind(options));

            return services;
        }
    }
}
