using Ecommerce_BE.Models;

namespace Ecommerce_BE.Repository
{
  public class StoreRepository : RepositoryBase<Store>
  {
    private readonly DbEContext _dbContext;
    public StoreRepository(DbEContext dbContext) : base(dbContext) 
    {
      _dbContext = dbContext;
    }
  }
}
