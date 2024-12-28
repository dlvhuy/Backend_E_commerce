using System.Linq.Expressions;

namespace Ecommerce_BE.Repository.Abstractions
{
  public interface IRepositoryBase<T>
  {
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    IQueryable<T> FindItemByCriteria(Expression<Func<T, bool>>? predicate = null);

    T GetItemByCriteria(Expression<Func<T, bool>>? predicate = null);
  }
}
