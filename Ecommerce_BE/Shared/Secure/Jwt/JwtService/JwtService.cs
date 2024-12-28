using Ecommerce_BE.Shared.Secure.Jwt.JwtConfiguration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_BE.Shared.Secure.Jwt.JwtService
{
  public class JwtService : IJwtService
  {
      private readonly JwtOption jwtOption = new JwtOption();

    public JwtService(IConfiguration configuration)
    {
      configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);
    }
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

      var token = new JwtSecurityToken(
        issuer: jwtOption.Issuer,
        audience: jwtOption.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(jwtOption.ExpireMin),
        signingCredentials: creds
    );

      string tokentString = new JwtSecurityTokenHandler().WriteToken(token);
      return tokentString;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
      var Key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
      var jwtToken = securityToken as JwtSecurityToken;

      if (jwtToken == null) throw new SecurityTokenException("Invalid Token");

      return principal;
    }
  }
}
