using Ecommerce_BE.Models;
using Ecommerce_BE.Repository.Abstractions;
using Ecommerce_BE.Shared.Exceptions.UserExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Ecommerce_BE.Repository
{
  
  public  class RepositoryBase<T> : IRepositoryBase<T> where T : class
  {
    private readonly DbEContext _context;
    private readonly DbSet<T> _dbSet;
    public RepositoryBase(DbEContext context)
    {
      _context = context;
      _dbSet = _context.Set<T>();
    }
    public void Add(T entity)
    {
       _dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
      _dbSet.Remove(entity);
    }
    public void Update(T entity)
    {
      _dbSet.Update(entity);
    }

    public IQueryable<T> FindItemByCriteria(Expression<Func<T, bool>>? predicate = null)
    {
      IQueryable<T> item = _dbSet;
      if (predicate != null)
        item = item.Where(predicate).AsNoTracking();
      return item;
    }

    public T GetItemByCriteria(Expression<Func<T, bool>>? predicate = null)
    {
      T? item = FindItemByCriteria(predicate)
                .AsNoTracking()
                .SingleOrDefault();
      if (item == null)
        throw new UserException.UserNotFoundException();

      return item;
    }

    public Expression<Func<T, bool>> BuildPredicate(string propertyName, string comparisonType, object value)
    {
      // Lấy tham số đầu vào, ví dụ: "Product p"
      var parameter = Expression.Parameter(typeof(T), "p");

      // 2. Lấy thông tin thuộc tính qua tên (propertyName)
      var property = typeof(T).GetProperty(propertyName);

      if (property == null)
        throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T)}'.");

      // 3. Lấy kiểu thực sự của thuộc tính
      var propertyType = property.PropertyType;

      // 4. Nếu thuộc tính là Nullable<T>, lấy kiểu T bên trong

      // 5. Chuyển đổi giá trị sang kiểu của thuộc tính
      var convertedValue = Convert.ChangeType(value, propertyType);

      // 6. Tạo biểu thức truy cập thuộc tính (x.Age)
      var propertyAccess = Expression.Property(parameter, property);

      // 7. Tạo constant từ giá trị đã chuyển đổi
      var constant = Expression.Constant(convertedValue, property.PropertyType);

      // So sánh (ví dụ: p.Price > 20)
      Expression comparison;
      switch (comparisonType)
      {
        case "Equals":
          comparison = Expression.Equal(propertyAccess, constant);
          break;
        case "GreaterThan":
          comparison = Expression.GreaterThan(propertyAccess, constant);
          break;
        case "LessThan":
          comparison = Expression.LessThan(propertyAccess, constant);
          break;
        case "Contains":
          // Với Contains, phải dùng Method Call
          var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
          comparison = Expression.Call(propertyAccess, containsMethod, constant);
          break;
        default:
          throw new NotSupportedException($"Comparison type '{comparisonType}' is not supported.");
      }

      // Kết hợp tham số đầu vào với biểu thức, tạo thành lambda
      return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    public Expression<Func<T, bool>> CombinePredicates(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
      var parameter = Expression.Parameter(typeof(T), "p");

      var combined = Expression.AndAlso(
          Expression.Invoke(first, parameter),
          Expression.Invoke(second, parameter)
      );

      return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

  }
}
