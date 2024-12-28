namespace Ecommerce_BE.Shared.ResponseModels
{
  public class Result
  {
    public string _message { get; set; } = string.Empty;

    public bool _isSuccess { get; set; }

    public Result() { }
    public Result(bool isSuccess, string message)
    {
      _isSuccess = isSuccess;
      _message = message;
      
    }
    public static Result Success(string message = "Success")
    {

      return new Result
      {
       
        _isSuccess = true,
        _message = message
      };
    }



    public static Result Failure(string message = "failed")
    {
      return new Result
      {
        _message = message,
        _isSuccess = false
      };
    }
  }
}
