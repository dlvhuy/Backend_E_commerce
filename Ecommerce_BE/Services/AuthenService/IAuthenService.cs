using Ecommerce_BE.Services.AuthenService.Dto;

namespace Ecommerce_BE.Services.AuthenService
{
  public interface IAuthenService
  {
    bool Register(DtoRegister register);
    string Login(DtoLogin login);

    void Logout();
  }
}
