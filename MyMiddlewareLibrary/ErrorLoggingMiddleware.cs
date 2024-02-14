using Microsoft.AspNetCore.Http;

namespace MyMiddlewareLibrary
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public ErrorLoggingMiddleware(RequestDelegate next, string logFilePath)
        {
            _next = next;
            _logFilePath = logFilePath;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogErrorAsync(context, ex);
                throw;
            }
        }

        private async Task LogErrorAsync(HttpContext context, Exception exception)
        {
            string logMessage = $"[{DateTime.UtcNow}] {context.Request.Path}: {exception.Message}";
            await WriteToLogFile(logMessage);
        }

        private async Task WriteToLogFile(string logMessage)
        {
            try
            {
                await File.AppendAllTextAsync(_logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception) {}
        }
    }
}

