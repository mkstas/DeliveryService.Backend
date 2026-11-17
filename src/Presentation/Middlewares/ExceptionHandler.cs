using System.Net;
using Application.DTOs.Common;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Presentation.Middlewares
{
    /// <summary>
    /// Middleware that converts unhandled exceptions into JSON error responses.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public class ExceptionHandler(RequestDelegate next)
    {
        private static bool IsGenericException(Exception ex, Type definition)
        {
            return ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == definition;
        }

        /// <summary>
        /// Handles an HTTP request and converts unhandled exceptions into JSON error responses.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task that represents the asynchronous request handling.</returns>
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
                    DomainException _ when IsGenericException(ex, typeof(ConflictException<>)) => HttpStatusCode.Conflict,
                    DomainException _ when IsGenericException(ex, typeof(DeleteConstraintException<>)) => HttpStatusCode.Conflict,
                    DomainException _ => HttpStatusCode.BadRequest,
                    InvalidOperationException _ => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };

                var response = new ErrorResponse((int)statusCode, ex.Message);

                context.Response.StatusCode = (int)statusCode;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
