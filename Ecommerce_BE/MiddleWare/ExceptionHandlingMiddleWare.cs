
using Ecommerce_BE.Shared.Exceptions.UserExceptions;
using Ecommerce_BE.Shared.ResponseModels;
using System.Text.Json;

namespace Ecommerce_BE.MiddleWare
{
  public class ExceptionHandlingMiddleWare 
  {
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleWare(RequestDelegate next)
    {
      _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
      var statusCode = GetStatusCode(exception);

      var response = new
      {
        status = statusCode,
        detail = exception.Message,
      };

      httpContext.Response.ContentType = "application/json";

      httpContext.Response.StatusCode = statusCode;

      var result = ResultT<int>.Failure(response.status, response.detail);
      await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result));
    }
      
    private static int GetStatusCode(Exception exception) =>
      exception switch
      {
        //UserException
        UserException.UserNotFoundException => StatusCodes.Status404NotFound,
        UserException.UserNotFoundExceptionById => StatusCodes.Status404NotFound,
        UserException.AlreadyHaveFieldException => StatusCodes.Status400BadRequest,
        UserException.FieldNotExistException => StatusCodes.Status400BadRequest,
        UserException.FieldNotMatch => StatusCodes.Status400BadRequest,



        _ => StatusCodes.Status500InternalServerError
      };

  }
}
