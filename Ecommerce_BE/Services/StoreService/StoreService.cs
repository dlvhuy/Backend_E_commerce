using AutoMapper;
using Ecommerce_BE.Models;
using Ecommerce_BE.Repository;
using Ecommerce_BE.Repository.Abstractions;
using Ecommerce_BE.Services.StoreService.Dtos.Category;
using Ecommerce_BE.Services.StoreService.Dtos.Product;
using Ecommerce_BE.Services.StoreService.Dtos.Store;
using Ecommerce_BE.Shared.Files;
using Ecommerce_BE.Shared.ResponseModels;
using Ecommerce_BE.Shared.Urldto;
using System.Linq.Expressions;


namespace Ecommerce_BE.Services.StoreService
{
  public class StoreService : IStoreService
  {
    private readonly StoreRepository _storeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    private readonly ProductRepository _productRepository;
    private readonly UserRepository _userRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public StoreService(StoreRepository storeRepository,
     IUnitOfWork unitOfWork,
     IFileService fileService,
     ProductRepository productRepository,
     UserRepository userRepository,
     CategoryRepository categoryRepository,
     IMapper mapper
     )
    {
      _storeRepository = storeRepository;
      _unitOfWork = unitOfWork;
      _fileService = fileService;
      _productRepository = productRepository;
      _userRepository = userRepository;
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    public Category AddCategory(dtoCategory category)
    {
      try
      {
        _unitOfWork.BeginTransaction();

        Category categoryNew = new Category()
        {
          Name = category.Name,
        };
        _categoryRepository.Add(categoryNew);
        _unitOfWork.Commit();

        return categoryNew;
      }
      catch (Exception ex)
      {
        _unitOfWork.RollBack();
        throw new Exception();
      }


    }

    public dtoProductResponse AddProduct(dtoProduct product, int idUser)
    {
      try
      {
        _unitOfWork.BeginTransaction();

        var user = _userRepository.GetItemByCriteria(u => u.Id == idUser);
        if (!user.isSeller) return null;


        var productNew = _mapper.Map<Product>(product);

        //Product productNew = new Product()
        //{
        //  Name = product.Name,
        //  Description = product.Description,
        //  Image = _fileService.UploadFileAsync(product.Image),
        //  Quantity = product.Quantity,
        //  SellingPrice = product.SellingPrice,
        //  CostPrice = product.CostPrice,
        //  CreateTime = DateTime.UtcNow,
        //  IdStore = product.IdStore,
        //  IdCategory = product.IdCategory,
        //};

        _productRepository.Add(productNew);
        _unitOfWork.Commit();

        var result = _mapper.Map<dtoProductResponse>(productNew);

        return result;
      }
      catch (Exception ex)
      {
        _unitOfWork.RollBack();
        throw new Exception();
      }
    }

    public Product DeleteProduct(int idProduct, int idUser)
    {
      throw new NotImplementedException();
    }

    public List<Category> GetCategorys()
    {
      var listCategory = _categoryRepository.FindItemByCriteria().ToList();

      return listCategory;


    }

    public Product GetProduct(int idProduct)
    {
      _unitOfWork.BeginTransaction();

      var item = _productRepository.GetItemByCriteria(x => x.Id == idProduct);

      return item;
    }

    public List<dtoProductResponse> GetProducts(int idStore, List<UrlParam> urlParams, int pageNumber, int pageSize)
    {

      Expression<Func<Product, bool>> combinedPredicate = null;
      foreach (var item in urlParams)
      {
        var predicate = _productRepository.BuildPredicate(item.propertyName, item.comparisonType, item.value);

        combinedPredicate = combinedPredicate == null
            ? predicate
            : _productRepository.CombinePredicates(combinedPredicate, predicate);
      }

      combinedPredicate = combinedPredicate == null
           ? u => u.IdStore == idStore
           : _productRepository.CombinePredicates(combinedPredicate, u => u.IdStore == idStore);

      combinedPredicate = _productRepository.CombinePredicates(combinedPredicate, u => u.IdStore == idStore);
      var listProduct = _productRepository.FindItemByCriteria(combinedPredicate);

      var pagedData = Page.ApplyPaging(listProduct, new Page(pageNumber, pageSize)).ToList();

      var listProductR = _mapper.Map<List<dtoProductResponse>>(pagedData);
      return listProductR;

    }

    public List<Store> GetStores(int idUser)
    {
      throw new NotImplementedException();
    }

    public Store RegisterStore(dtoStore store, int idUser)
    {
      try
      {
        if (idUser != store.idUser) return null;

        _unitOfWork.BeginTransaction();


        Store storeNew = new Store()
        {
          Address = store.Address,
          Description = store.Description,
          Image = _fileService.UploadFileAsync(store.Image),
          Name = store.Name,
          Rate = 0,
          IdUser = idUser,
          CreateTime = DateTime.UtcNow,
        };

        _storeRepository.Add(storeNew);
        _unitOfWork.SaveChanged();

        var userUpdate = _userRepository.GetItemByCriteria(u => u.Id == idUser);

        userUpdate.isSeller = true;
        _userRepository.Update(userUpdate);

        _unitOfWork.Commit();

        return storeNew;
      }
      catch (Exception ex)
      {
        _unitOfWork.RollBack();
        throw new Exception();
      }
    }

    public Product UpdateProduct(int idProduct, dtoProduct product, int idUser)
    {
      try
      {

        _unitOfWork.BeginTransaction();

        var item = _productRepository.GetItemByCriteria(x => x.Id == idProduct);

        if (item.Store.IdUser != idUser) return null;

        Product productUpdate = new Product()
        {
          Name = product.Name,
          Description = product.Description,
          Image = _fileService.DeleteFile(product.Image, item.Image),
          Quantity = product.Quantity,
          SellingPrice = product.SellingPrice,
          CostPrice = product.CostPrice,
          CreateTime = item.CreateTime,
          IdStore = item.IdStore,
          IdCategory = item.IdCategory,

        };

        _productRepository.Update(productUpdate);
        _unitOfWork.Commit();

        return productUpdate;
      }
      catch (Exception ex)
      {
        _unitOfWork.RollBack();
        throw new Exception();
      }
    }
  }
}
