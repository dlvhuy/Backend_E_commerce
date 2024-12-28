namespace Ecommerce_BE.Shared.Exceptions
{
  public class BadRequestException : Exception
  {
    public BadRequestException(string message) : base(message) { }
  }
}
