using Ecommerce_BE.Services.AuthenService;
using Ecommerce_BE.Services.AuthenService.Dto;
using Ecommerce_BE.Shared.ResponseModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Ecommerce_BE.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenController : Controller
  {
    private readonly IAuthenService _authenService;
    public AuthenController(IAuthenService authenService)
    {
      _authenService = authenService;
    }
    [HttpPost("register")]
    public IActionResult Register(DtoRegister register)
    {
      var isSuccess = _authenService.Register(register);
      var result = Result.Success();
      return Ok(result);
    }
    [HttpPost("login")]
    public IActionResult Login(DtoLogin login)
    {
      var token = _authenService.Login(login);
      ResultT<string> result = ResultT<string>.Success(token, "Login successfully.");


      return Ok(result);
    }


  }
}
