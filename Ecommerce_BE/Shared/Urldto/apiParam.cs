using System.Net;

namespace Ecommerce_BE.Shared.Urldto
{
  public class apiParam
  {
    public int NumberPage { get; set; }

    public int SizePage { get; set; }

    public List<UrlParam> UrlParams { get; set; }

    public static apiParam ParseUrlParameters(HttpContext context)
    {
      // Lấy query string từ URL
      var queryParams = context.Request.Query;

      int numberPage = 1;
      int sizePage = 10;
      var urlParams = new List<UrlParam>();

      foreach (var queryParam in queryParams)
      {

        if (queryParam.Key.Equals("numberPage", StringComparison.OrdinalIgnoreCase))
        {
          numberPage = int.TryParse(queryParam.Value, out var parsedNumberPage) ? parsedNumberPage : 1;
          continue;
        }

        if (queryParam.Key.Equals("sizePage", StringComparison.OrdinalIgnoreCase))
        {
          sizePage = int.TryParse(queryParam.Value, out var parsedSizePage) ? parsedSizePage : 10;
          continue;
        }
        // Dữ liệu từng tham số dạng: name:eq:John
        var values = queryParam.Value.ToString().Split(':');

        if (values.Length == 3) // Chỉ xử lý nếu có đúng 3 phần tử
        {
          urlParams.Add(new UrlParam
          {
            propertyName = values[0],
            comparisonType = values[1],
            value = Convert.ChangeType(values[2], typeof(object)) 
          });
        }
      }

      return new apiParam
      {
        NumberPage = numberPage,
        SizePage = sizePage,
        UrlParams = urlParams
      };
    }

  }
}
