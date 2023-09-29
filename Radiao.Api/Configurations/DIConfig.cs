using Radiao.Data.MySql;
using Radiao.Data.RadioBrowser;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Impl;
using Radiao.Domain.Services;
using Radiao.Domain.Services.Notifications;

namespace Radiao.Api.Configurations
{
    public static class DIConfig
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            // repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStationRepository, StationRepository>();

            // services
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUserService, UserService>();

            // others
            services.AddAutoMapper(typeof(Program).Assembly);

            return services;
        }
    }
}
