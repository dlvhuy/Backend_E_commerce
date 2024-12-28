namespace Ecommerce_BE.Shared.ResponseModels
{
  public class Page
  {
      public int PageNumber { get; set; } = 1; // Default to the first page
      public int PageSize { get; set; } = 10; // Default page size

      public int Skip => (PageNumber - 1) * PageSize;
      public int Take => PageSize;

      public Page() { }

      public Page(int pageNumber, int pageSize)
      {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 10 ? 10 : pageSize;
      }

      public static IQueryable<T> ApplyPaging<T>(IQueryable<T> query, Page paging)
      {
        return query.Skip(paging.Skip).Take(paging.Take);
      
      }

  }
}
