using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Radiao.Data.MySql;
using Radiao.Domain.Repositories;
using Radiao.Domain.Services;
using Radiao.Domain.Services.Impl;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var jwtkey = builder.Configuration.GetSection("JwtConfig").GetValue<string>("JwtSecret");

builder.Services.AddAuthentication(auth =>
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

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
