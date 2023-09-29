using Radiao.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.ConfigureDependencyInjection();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
