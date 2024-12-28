using Microsoft.AspNetCore.Http.HttpResults;

namespace Ecommerce_BE.Shared.Exceptions.UserExceptions
{
  public static class UserException
  {
    public class UserNotFoundExceptionById : NotFoundExeption
    {
      public UserNotFoundExceptionById(int UserId) : base($"NotFound {UserId} User") { }
    }

    public class UserNotFoundException : NotFoundExeption
    {
      public UserNotFoundException() : base($"NotFound User") { }
    }

    public class AlreadyHaveFieldException : BadRequestException
    {
      public AlreadyHaveFieldException(string fieldName,string value) : base($"The field {fieldName}: {value}  already exists.") {}

    }

    public class FieldNotExistException : NotFoundExeption
    {
      public FieldNotExistException(string fieldName) : base($"{fieldName} not exists.") { }

    }

    public class FieldNotMatch : BadRequestException
    {
      public FieldNotMatch(string fieldName) : base($"{fieldName} Incorrect.") { }

    }
  }
}
//