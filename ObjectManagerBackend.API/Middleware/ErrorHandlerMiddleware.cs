using ObjectManagerBackend.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ObjectManagerBackend.API.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions from application
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exceptions setting the appropriate status code
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="ex">Exception to handle</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;

            switch (ex)
            {
                case BadRequestException e:
                    status = HttpStatusCode.BadRequest;
                    break;
                case KeyConflictException e:
                    status = HttpStatusCode.Conflict;
                    break;
                case NotFoundException e:
                    status = HttpStatusCode.NotFound;
                    break;
                // More exceptions types...
                default:
                    status = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = ex?.Message, stackTrace = ex?.StackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(result);
        }
    }
}
