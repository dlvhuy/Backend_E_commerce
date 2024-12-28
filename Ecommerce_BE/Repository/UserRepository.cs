using Ecommerce_BE.Models;

namespace Ecommerce_BE.Repository
{
  public class UserRepository : RepositoryBase<User>
  {
    private readonly DbEContext _dbContext;
    public UserRepository(DbEContext dbContext) : base(dbContext)
      => _dbContext = dbContext;

  }
}
