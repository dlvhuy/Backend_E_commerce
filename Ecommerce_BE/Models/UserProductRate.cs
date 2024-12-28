namespace Ecommerce_BE.Models
{
  public class UserProductRate
  { 
    public int IdUser { get; set; }

    public int ProductId { get; set; }


    public int Rate { get; set; }

    public virtual Product Product { get; set; }

    public virtual User ProductUser { get; set; }
  }
}
