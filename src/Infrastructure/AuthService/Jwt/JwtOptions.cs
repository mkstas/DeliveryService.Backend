namespace Infrastructure.AuthService.Jwt
{
    /// <summary>
    /// Configuration options for JWT generation.
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Gets or sets the secret key used to sign the JWT.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of days until the JWT expires.
        /// </summary>
        public int ExpiresDays { get; set; }
    }
}
