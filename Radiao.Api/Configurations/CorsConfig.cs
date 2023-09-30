namespace Radiao.Api.Configurations
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            return services;
        }

        public static WebApplication AddCors(this WebApplication app)
        {
            app.UseCors("DevPolicy");

            return app;
        }
    }
}
