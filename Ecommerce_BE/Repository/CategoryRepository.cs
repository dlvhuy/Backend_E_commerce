using Ecommerce_BE.Models;

namespace Ecommerce_BE.Repository
{
  public class CategoryRepository : RepositoryBase<Category>
  {
    private readonly DbEContext _dbContext;
    public CategoryRepository(DbEContext dbEContext) : base(dbEContext) 
    {
      _dbContext = dbEContext;
    }
  }
}
