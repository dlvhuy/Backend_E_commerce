
using AutoMapper;
using Ecommerce_BE.MiddleWare;
using Ecommerce_BE.Models;
using Ecommerce_BE.Repository;
using Ecommerce_BE.Repository.Abstractions;
using Ecommerce_BE.Services.AuthenService;
using Ecommerce_BE.Services.StoreService;
using Ecommerce_BE.Services.UserService;
using Ecommerce_BE.Shared.Caching;
using Ecommerce_BE.Shared.Files;
using Ecommerce_BE.Shared.Profiles;
using Ecommerce_BE.Shared.Secure.Bcrypt;
using Ecommerce_BE.Shared.Secure.CORS;
using Ecommerce_BE.Shared.Secure.Jwt.JwtConfiguration;
using Ecommerce_BE.Shared.Secure.Jwt.JwtService;
using Ecommerce_BE.Shared.Secure.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

namespace Ecommerce_BE
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwagger(builder.Configuration);
     

      builder.Services.AddJwtAuthentication(builder.Configuration);
      builder.Services.AddCorsConfiguration(builder.Configuration);
      builder.Services.AddDbContext<DbEContext>(option =>
      {
        option.UseSqlServer(builder.Configuration.GetConnectionString("DbEContextConnectionString"));
      });
      builder.Services.AddMemoryCache();

      builder.Services.AddScoped<IAuthenService, AuthenService>();
      builder.Services.AddScoped<IStoreService, StoreService>();
      builder.Services.AddScoped<IUserService, UserService>();
      builder.Services.AddScoped<IPasswordService,PasswordService>();
      builder.Services.AddScoped<IFileService, FileService>();
      builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
      builder.Services.AddScoped(typeof(ICachingService<>), typeof(CachingService<>));
      builder.Services.AddScoped<IJwtService, JwtService>();

      builder.Services.AddScoped<StoreRepository>();
      builder.Services.AddScoped<ProductRepository>();
      builder.Services.AddScoped<UserRepository>();
      builder.Services.AddScoped<CategoryRepository>();
      builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      builder.Services.AddAutoMapper((cfg )=>
      {
        var serviceProvider = builder.Services.BuildServiceProvider();
        cfg.AddProfile(new ProductProfile(serviceProvider.GetRequiredService<IFileService>()));
      });

      
      var app = builder.Build();

      //if (app.Environment.IsDevelopment())
      //{
      //  app.UseSwagger();
      //  app.UseSwaggerUI();
      //}
      app.UseCors("AllowOrigins");
      app.UseStaticFiles(new StaticFileOptions
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
        RequestPath = "/image"
      });

      app.UseMiddleware<ExceptionHandlingMiddleWare>();
      app.UseRouting();

      app.UseAuthentication();
      // Configure the HTTP request pipeline.
      app.UseMiddleware<TokenValidationMiddleWare>();
    
      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
