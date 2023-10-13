using Radiao.Data.MySql;
using Radiao.Data.RadioBrowser;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services.Impl;
using Radiao.Domain.Services;
using Radiao.Domain.Services.Notifications;
using Radiao.Data.Email;

namespace Radiao.Api.Configurations
{
    public static class DIConfig
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            // repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITemplateEmailRepository, TemplateEmailRepository>();

            // services
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITemplateEmailService, TemplateEmailService>();
            services.AddScoped<IEmailService, EmailService>();

            // others
            services.AddAutoMapper(typeof(Program).Assembly);

            return services;
        }
    }
}
