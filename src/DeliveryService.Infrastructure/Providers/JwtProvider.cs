using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryService.Infrastructure.Providers
{
    /// <summary>
    /// Configuration options for JWT generation.
    /// </summary>
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresDays { get; set; } = 0;
    }

    /// <summary>
    /// Provides generation of JWT access tokens.
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The generated JWT access token.</returns>
        string GenerateAccessToken(Guid userId);
    }

    /// <summary>
    /// Generates JWT access tokens based on <see cref="JwtOptions"/>.
    /// </summary>
    /// <param name="options">The JWT configuration options.</param>
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        private static Claim[] CreateClaims(Guid id)
        {
            return
            [
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            ];
        }

        /// <summary>
        /// Generates a JWT access token for the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The generated JWT access token.</returns>
        public string GenerateAccessToken(Guid userId)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                claims: CreateClaims(userId),
                expires: DateTime.UtcNow.AddDays(_options.ExpiresDays),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
