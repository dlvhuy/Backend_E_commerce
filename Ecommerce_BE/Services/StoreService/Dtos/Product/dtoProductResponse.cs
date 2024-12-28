namespace Ecommerce_BE.Services.StoreService.Dtos.Product
{
  public class dtoProductResponse
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public int CostPrice { get; set; } = 0;
    public int SellingPrice { get; set; } = 0;

    public int Quantity { get; set; }

    public int IdStore { get; set; }

    public int IdCategory { get; set; }

    public string CreateTime { get; set; }
  }
}
