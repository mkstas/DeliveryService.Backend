namespace Application.Abstractions.Auth
{
    /// <summary>
    /// Provides generation of JWT access tokens.
    /// </summary>
    public interface IAuthorizationJwtService
    {
        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The generated JWT access token.</returns>
        string GenerateAccessToken(Guid userId);
    }
}
