using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ecommerce_BE.Shared.Secure.Jwt.JwtConfiguration
{
  public static class  jwtExtension 
  {
    public static void AddJwtAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        JwtOption jwtOption = new JwtOption();
        configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          ValidateAudience = false,
          ValidateIssuer = false,
          ClockSkew = TimeSpan.Zero,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey)),
        };

        //options.Events = new JwtBearerEvents
        //{
        //  OnChallenge = context =>
        //  {
        //    context.HandleResponse(); // Ngăn chặn redirect
        //    context.Response.StatusCode = 401; // Trả về Unauthorized
        //    context.Response.ContentType = "application/json";
        //    return context.Response.WriteAsync("{\"error\": \"Unauthorized\"}");
        //  }
        //};
      });
    }
  }
}
