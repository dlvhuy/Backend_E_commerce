namespace Ecommerce_BE.Shared.ResponseModels
{
  public class ResultT<T>
  {
    public string _message { get; set; } = string.Empty;
    public T? _data { get; set; }
    public bool _isSuccess { get; set; }

    public ResultT() { }
    public ResultT(bool isSuccess, string message, T data)
    {
      _isSuccess = isSuccess;
      _message = message;
      _data = data;
    }
    public static ResultT<T> Success(T data, string message = "Success")
    {
      
      return new ResultT<T>
      {
        _data = data,
        _isSuccess = true,
        _message = message
      };
    }



    public static ResultT<T> Failure(T data, string message = "failed")
    {
      return new ResultT<T>
      {
        _message = message,
        _data = data,
        _isSuccess = false
      };
    }
  }
}
