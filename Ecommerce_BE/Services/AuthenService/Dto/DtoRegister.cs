namespace Ecommerce_BE.Services.AuthenService.Dto
{
  public class DtoRegister
  {
    public required string UserName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string PhoneNumber { get; set; }
  }
}
