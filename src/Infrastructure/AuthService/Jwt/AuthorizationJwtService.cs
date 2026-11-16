using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstractions.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.AuthService.Jwt
{
    /// <summary>
    /// Implementation of <see cref="IAuthorizationJwtService"/>.
    /// </summary>
    public class AuthorizationJwtService(IOptions<AuthorizationJwtOptions> options) : IAuthorizationJwtService
    {
        private readonly AuthorizationJwtOptions _options = options.Value;

        private static Claim[] CreateClaims(Guid id)
        {
            return
            [
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            ];
        }

        /// <inheritdoc />
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
