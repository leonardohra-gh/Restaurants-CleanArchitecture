using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.API.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                await next.Invoke(context);
            }
            finally
            {
                stopWatch.Stop();
                if (stopWatch.ElapsedMilliseconds >= 4000)
                    logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                        context.Request.Method,
                        context.Request.Path,
                        stopWatch.ElapsedMilliseconds);
            }
        }
    }
}