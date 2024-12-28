namespace Ecommerce_BE.Repository.Abstractions
{
  public interface IUnitOfWork
  {
    void Commit();
    void BeginTransaction();
    void RollBack();
    void SaveChanged();
    Task SaveChangedAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollBackAsync(CancellationToken cancellationToken = default);
  }
}
