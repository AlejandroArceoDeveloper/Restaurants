
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.MiddleWares
{
    public class ErrorHandlingMiddleWare(ILogger<ErrorHandlingMiddleWare> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundEx)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context .Response.WriteAsync(notFoundEx.Message); 
            }
          
            catch (Exception ex)
            {
                // Log the exception
                logger.LogError(ex, "An error occurred while processing the request.");

                // Set the response status code and content type
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");


            }
        }
    }
}
