namespace Application.DTOs.Common
{
    /// <summary>
    /// Represents an error returned to the client.
    /// </summary>
    /// <param name="Status">The HTTP status code.</param>
    /// <param name="Message">The error message.</param>
    public record ErrorResponse(int Status, string Message);
}
