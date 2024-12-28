using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_BE.Controllers
{
  public class UserController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
