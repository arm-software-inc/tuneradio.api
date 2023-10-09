using Serilog;

namespace Radiao.Api.Configurations
{
    public static class LogConfig
    {
        public static Serilog.ILogger ConfigSerilog(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
