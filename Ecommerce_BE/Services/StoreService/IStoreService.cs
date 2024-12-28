using Ecommerce_BE.Models;
using Ecommerce_BE.Services.StoreService.Dtos.Category;
using Ecommerce_BE.Services.StoreService.Dtos.Product;
using Ecommerce_BE.Services.StoreService.Dtos.Store;
using Ecommerce_BE.Shared.Urldto;

namespace Ecommerce_BE.Services.StoreService
{
    public interface IStoreService
  {
    Store RegisterStore(dtoStore store, int idUser);

    public List<dtoProductResponse> GetProducts(int idStore, List<UrlParam> urlParams, int pageNumber, int pageSize);

    public List<Store> GetStores(int idUser);

    public dtoProductResponse AddProduct(dtoProduct product, int idStore);

    public Product UpdateProduct(int idProduct,dtoProduct product,int idUser);

    public Product DeleteProduct(int idProduct,int idUser);

    public Product GetProduct(int idProduct);

    public Category AddCategory(dtoCategory category);

    public List<Category> GetCategorys();
  }
}
