namespace Ecommerce_BE.Shared.TIme
{
  public static class Time
  {
    public static string GetTimeAgo(DateTime createdAt)
    {
      var timeSpan = DateTime.UtcNow - createdAt;

      if (timeSpan.TotalMinutes < 1)
        return "Vừa xong";
      if (timeSpan.TotalHours < 1)
        return $"{timeSpan.Minutes} phút trước";
      if (timeSpan.TotalDays < 1)
        return $"{timeSpan.Hours} giờ trước";
      if (timeSpan.TotalDays < 7)
        return $"{timeSpan.Days} ngày trước";

      return createdAt.ToString("dd/MM/yyyy");
    }

    public static string GetTimeToString(DateTime time)
    {
      string date = time.ToString("yyyy-MM-dd");
      return date;

    }
  }
}
