namespace Ecommerce_BE.Shared.Secure.CORS
{
  public static class CorsExtension
  {
    public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {


      services.AddCors(option =>
      {
        option.AddPolicy("AllowOrigins", (builder =>
           builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
          ));

      });
    
    }
}
}
