using Application.DTOs.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Common
{
    /// <summary>
    /// Builds error responses for invalid request models.
    /// </summary>
    internal static class ErrorResponseFactory
    {
        /// <summary>
        /// Builds an error response from the specified model state.
        /// </summary>
        /// <param name="modelState">The model state containing validation errors.</param>
        /// <returns>An <see cref="ErrorResponse"/> with a combined validation message.</returns>
        public static ErrorResponse FromModelState(ModelStateDictionary modelState)
        {
            var message = string.Join("; ", modelState
                .Where(entry => entry.Value is not null && entry.Value.Errors.Count > 0)
                .Select(entry =>
                {
                    var errors = string.Join(", ", entry.Value!.Errors
                        .Select(error => error.ErrorMessage)
                        .Where(error => !string.IsNullOrWhiteSpace(error)));

                    return string.IsNullOrWhiteSpace(errors) ? entry.Key : $"{entry.Key}: {errors}";
                }));

            return new ErrorResponse(StatusCodes.Status400BadRequest, message);
        }
    }
}
