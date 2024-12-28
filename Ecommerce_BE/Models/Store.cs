namespace Ecommerce_BE.Models
{
  public class Store
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }
    public string Description { get; set; }

    public string Address { get; set; }

    public int Rate { get; set; }

    public int IdUser { get; set; }

    public DateTime CreateTime { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

  }
}
