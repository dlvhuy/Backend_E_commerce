using Ecommerce_BE.Shared.Secure.Jwt.JwtConfiguration;
using System.Security.Claims;

namespace Ecommerce_BE.Shared.Secure.Jwt.JwtService
{
  public interface IJwtService
  {
    string GenerateAccessToken(IEnumerable<Claim> claims);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
  }
}
