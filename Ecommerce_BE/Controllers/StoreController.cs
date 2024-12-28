using Ecommerce_BE.Models;
using Ecommerce_BE.Services.AuthenService;
using Ecommerce_BE.Services.AuthenService.Dto;
using Ecommerce_BE.Services.StoreService;
using Ecommerce_BE.Services.StoreService.Dtos.Category;
using Ecommerce_BE.Services.StoreService.Dtos.Product;
using Ecommerce_BE.Services.StoreService.Dtos.Store;
using Ecommerce_BE.Shared.ResponseModels;
using Ecommerce_BE.Shared.Secure.Jwt.JwtService;
using Ecommerce_BE.Shared.Urldto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace Ecommerce_BE.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class StoreController : Controller
  {
    private readonly IStoreService _storeService;
    private readonly IJwtService _jwtService;

    public StoreController(IStoreService storeService,
      IJwtService jwtService
      )
    {
      _storeService = storeService;
      _jwtService = jwtService;
    }

    [HttpPost("registerStore")]
    public IActionResult RegisterStore([FromForm] dtoStore store)
    {

      var userIdFromToken = Convert.ToInt32(HttpContext.Items["UserId"]);

      var isSuccess = _storeService.RegisterStore(store, userIdFromToken);
      var result = Result.Success();
      return Ok(result);
    }

    [HttpPost("Product")]
    public IActionResult AddProduct([FromForm] dtoProduct product)
    {

      var userIdFromToken = Convert.ToInt32(HttpContext.Items["UserId"]);

      var isSuccess = _storeService.AddProduct(product, userIdFromToken);
      ResultT<dtoProductResponse> result = ResultT<dtoProductResponse>.Success(isSuccess);
      return Ok(result);
    }

    [HttpPut("Product/{idProduct}")]
    public IActionResult UpdateProduct(int idProduct, dtoProduct product)
    {

      var userIdFromToken = (int)HttpContext.Items["UserId"];

      var isSuccess = _storeService.UpdateProduct(idProduct, product, userIdFromToken);
      ResultT<Product> result = ResultT<Product>.Success(isSuccess);
      return Ok(result);
    }


    [HttpDelete("Product/{idProduct}")]
    public IActionResult DeleteProduct(int idProduct)
    {

      var userIdFromToken = (int)HttpContext.Items["UserId"];

      var isSuccess = _storeService.DeleteProduct(idProduct, userIdFromToken);
      var result = Result.Success();
      return Ok(result);
    }


    [HttpGet("Products/{idStore}")]
    public IActionResult GetProducts(int idStore)
    {

      var userIdFromToken = Convert.ToInt32(HttpContext.Items["UserId"]);

      var param = apiParam.ParseUrlParameters(HttpContext);

      var isSuccess = _storeService.GetProducts(idStore, param.UrlParams, param.NumberPage, param.SizePage);
      ResultT<List<dtoProductResponse>> result = ResultT<List<dtoProductResponse>>.Success(isSuccess);
      return Ok(result);
    }

    [HttpGet("Category")]
    public IActionResult GetCategories()
    {
      var param = apiParam.ParseUrlParameters(HttpContext);

      var isSuccess = _storeService.GetCategorys();
      ResultT<List<Category>> result = ResultT<List<Category>>.Success(isSuccess);
      return Ok(result);
    }

    [HttpPost("Category")]
    public IActionResult AddCategory(dtoCategory dtoCategory)
    {
      var param = apiParam.ParseUrlParameters(HttpContext);

      var isSuccess = _storeService.AddCategory(dtoCategory);
      ResultT<Category> result = ResultT<Category>.Success(isSuccess);
      return Ok(result);
    }

  }
}
