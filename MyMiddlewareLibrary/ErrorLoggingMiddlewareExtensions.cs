using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMiddlewareLibrary
{
    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLoggingMiddleware(this IApplicationBuilder builder, string logFilePath)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>(logFilePath);
        }
    }
}
