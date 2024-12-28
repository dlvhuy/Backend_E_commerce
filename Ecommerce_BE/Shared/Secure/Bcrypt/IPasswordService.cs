namespace Ecommerce_BE.Shared.Secure.Bcrypt
{
  public interface IPasswordService
  {
    public string HashPassword(string password);
    public bool VerifyPassword(string hashedPassword, string password);
  }
}
