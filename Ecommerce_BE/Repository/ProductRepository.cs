using Ecommerce_BE.Models;

namespace Ecommerce_BE.Repository
{
  public class ProductRepository : RepositoryBase<Product>
  {
    private readonly DbEContext _dbContext;
    public ProductRepository(DbEContext dbContext) : base(dbContext) 
    {
      _dbContext = dbContext;
    }

  }
}
