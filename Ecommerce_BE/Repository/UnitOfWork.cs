using Ecommerce_BE.Models;
using Ecommerce_BE.Repository.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce_BE.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DbEContext _dbContext;
    private IDbContextTransaction _transaction;
    public UnitOfWork(DbEContext dbContext)
    {
      _dbContext = dbContext;
    }
    public void BeginTransaction()
    {
      _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
      try
      {
        SaveChanged();
        _transaction.Commit();
      }
      catch
      {
        RollBack();
        throw;
      }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
      try
      {
        await SaveChangedAsync();
        await _transaction.CommitAsync(cancellationToken);
      }
      catch
      {
        await RollBackAsync();
        throw;
      }
    }

  

    public void RollBack()
    {
      _transaction.Rollback();
    }

    public void SaveChanged()
    {
      _dbContext.SaveChanges();
    }

    public async Task SaveChangedAsync(CancellationToken cancellationToken = default)
    {
      await _dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task RollBackAsync(CancellationToken cancellationToken = default)
    {
      await _transaction.RollbackAsync(cancellationToken);
    }
  }
}
