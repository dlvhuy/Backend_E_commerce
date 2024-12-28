using System.Linq.Expressions;

namespace Ecommerce_BE.Shared.Urldto
{
  public class UrlParam
  {
    public string propertyName { get; set; }

    public string comparisonType { get; set; }

    public object value { get; set; }

   
  }
}
