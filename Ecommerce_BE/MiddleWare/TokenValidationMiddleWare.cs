using Ecommerce_BE.Shared.Caching;
using Ecommerce_BE.Shared.Secure.Jwt.JwtService;

namespace Ecommerce_BE.MiddleWare
{
  public class TokenValidationMiddleWare
  {
    private readonly RequestDelegate _next;

    public TokenValidationMiddleWare(RequestDelegate next)
    {
      _next = next;
    }
    public async Task Invoke(HttpContext context, ICachingService<string> cache, IJwtService jwt)
    {

      var path = context.Request.Path.Value;
      if (path.Contains("Authen/login") || path.Contains("Authen/register"))
      {
        // Bỏ qua middleware này và chuyển đến middleware tiếp theo
        await _next(context);
        return;
      }


      var authHeader = context.Request.Headers["Authorization"];

      if (string.IsNullOrEmpty(authHeader) || !authHeader.ToString().StartsWith("Bearer "))
      {
        // Redirect đến trang login nếu không có token
        context.Response.Redirect("Authen/login");
        return;
      }

      var userId = jwt.GetPrincipalFromExpiredToken(authHeader[0].Substring(7)).FindFirst(ClaimRole.Id)?.Value;


      if (userId == null || await cache.GetAsync<string>(userId.ToString()) == null)
      {

        context.Response.Redirect("Authen/login");
        return;
      }


      // Lưu token vào HttpContext.Items để sử dụng ở các nơi khác
      context.Items["UserId"] = userId;

      await _next(context);
    }
  }
}
