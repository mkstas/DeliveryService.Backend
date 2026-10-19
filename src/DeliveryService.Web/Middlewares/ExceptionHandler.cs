using System.Net;
using DeliveryService.Domain.Common.Abstracts;
using DeliveryService.Domain.Exceptions;

namespace DeliveryService.Web.Middlewares
{
    public class ExceptionHandler(RequestDelegate next)
    {
        private class ErrorResponse(int status, string message)
        {
            public int Status { get; set; } = status;
            public string Message { get; set; } = message;
        }

        private static bool IsGenericException(Exception ex, Type definition)
        {
            return ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == definition;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    ArgumentNullException _ => HttpStatusCode.BadRequest,
                    ArgumentOutOfRangeException _ => HttpStatusCode.BadRequest,
                    ArgumentException _ => HttpStatusCode.BadRequest,
                    FormatException _ => HttpStatusCode.BadRequest,
                    UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                    DomainException _ when IsGenericException(ex, typeof(NotFoundException<>)) => HttpStatusCode.NotFound,
                    DomainException _ when IsGenericException(ex, typeof(AlreadyExistsException<>)) => HttpStatusCode.Conflict,
                    InvalidOrderStatusTransitionException _ => HttpStatusCode.Conflict,
                    DomainException _ => HttpStatusCode.BadRequest,
                    InvalidOperationException _ => HttpStatusCode.InternalServerError,
                    _ => HttpStatusCode.InternalServerError
                };

                var response = new ErrorResponse((int)statusCode, ex.Message);

                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
