namespace Infrastructure.AuthService.Jwt
{
    /// <summary>
    /// Configuration options for JWT generation.
    /// </summary>
    public class AuthorizationJwtOptions
    {
        /// <summary>
        /// Gets the secret key used to sign the JWT.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets the number of days until the JWT expires.
        /// </summary>
        public int ExpiresDays { get; set; } = 0;
    }
}
