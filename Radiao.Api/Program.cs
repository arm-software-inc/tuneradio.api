using Radiao.Api.Configurations;
using Radiao.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigCors();

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.ConfigureDependencyInjection();

builder.Services.ConfigureSwagger();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.AddCors();

app.UseMiddleware(typeof(ExceptionMiddleware));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddSwagger();

app.Run();
