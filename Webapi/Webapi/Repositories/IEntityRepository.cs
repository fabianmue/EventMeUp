using System.Linq.Expressions;

namespace Webapi.Repositories;

public interface IEntityRepository<T>
{
  T? Find(Expression<Func<T, bool>> expression);

  Task<T?> FindAsync(Expression<Func<T, bool>> expression);

  void Add(T entity);

  void AddRange(IEnumerable<T> entities);

  void Update(T entity);

  void Delete(T entity);

  int SaveChanges();

  Task<int> SaveChangesAsync();
}