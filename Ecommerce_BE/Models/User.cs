namespace Ecommerce_BE.Models
{
  public class User
  {
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public bool isActive { get; set; } = true;

    public bool isSeller { get; set; } = false;

    public virtual Store Store { get; set; }

    public virtual ICollection<UserProductRate> ProductRates { get; set; } = new List<UserProductRate>(); 
  }
}
