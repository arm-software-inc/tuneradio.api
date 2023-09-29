using System.Text.Json;

namespace Radiao.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(
            RequestDelegate requestDelegate,
            ILogger<ExceptionMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptions(httpContext, ex);
            }
        }

        private async Task HandleExceptions(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro fatal!");

            context.Response.StatusCode = 500; // internal server error

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                errors = new List<string>()
            }));
        }
    }
}
