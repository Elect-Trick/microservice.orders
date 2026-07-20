using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Polly.Bulkhead;
using Polly.CircuitBreaker;
using Polly.Timeout;

namespace OrdersAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleWare> _logger;

        public ExceptionHandlingMiddleWare(RequestDelegate next, ILogger<ExceptionHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {

                await _next(httpContext);

            }
            catch (BrokenCircuitException ex)
            {
                httpContext.Response.StatusCode = 503;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"The service is currently unavailable. Please try again in 2 minutes."
                });
            }
            catch (TimeoutRejectedException ex)
            {
                httpContext.Response.StatusCode = 504;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"The request timed out. Please try again in a few seconds."
                });
            }
            catch (BulkheadRejectedException ex)
            {
                httpContext.Response.StatusCode = 504;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"Bulkhead is currently overloaded. Please try again later."
                });
            }
            catch (Exception e)
            {

                //Log the exception using the logger. 
                _logger.LogError(e.Message);

                if (e.InnerException is not null)
                {
                    _logger.LogError($"{e.InnerException.Message}");
                }

                //Internal Server Error
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"{e.Message}"
                });
            }
          
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleWare>();
        }
    }
}
