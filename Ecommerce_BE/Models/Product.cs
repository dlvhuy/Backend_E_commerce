namespace Ecommerce_BE.Models
{
  public class Product
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }
    public string Description { get; set; }
    public int Rate { get; set; } = 0;

    public int CostPrice { get; set; } = 0;
    public int SellingPrice { get; set; } = 0;

    public int Quantity { get; set; }

    public int IdStore { get; set; }

    public int IdCategory { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual Category Category { get; set; }

    public virtual Store Store { get; set; }

    public virtual ICollection<UserProductRate> ProductRates { get; set; } = new List<UserProductRate>();

  }
}
