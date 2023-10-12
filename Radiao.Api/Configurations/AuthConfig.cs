using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Radiao.Api.Configurations
{
    public static class AuthConfig
    {
        public static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtkey = configuration.GetSection("JwtConfig").GetValue<string>("JwtSecret");

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection ConfigureGoogleSignIn(this IServiceCollection services, IConfiguration configuration)
        {
            var clientId= configuration
                .GetSection("Authentication")
                .GetSection("Google")
                .GetValue<string>("ClientId");

            var clientSecret = configuration.GetSection("Authentication")
                .GetSection("Google")
                .GetValue<string>("ClientSecret");

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = clientId!;
                options.ClientSecret = clientSecret!;
            });

            return services;
        }
    }
}
