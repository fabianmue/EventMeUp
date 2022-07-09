using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Webapi.DatabaseContext;
using Webapi.Models;

namespace Webapi.Repositories;

public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
{
  protected readonly WebapiContext _context;

  protected readonly DbSet<T> _dbSet;

  public EntityRepository(WebapiContext context)
  {
    _context = context;
    _dbSet = _context.Set<T>();
  }

  public virtual T? Find(Expression<Func<T, bool>> expression)
  {
    return _dbSet.SingleOrDefault(expression);
  }

  public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> expression)
  {
    return _dbSet.Where(expression);
  }

  public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
  {
    return await _dbSet.SingleOrDefaultAsync(expression);
  }

  public void Add(T entity)
  {
    _dbSet.Add(entity);
  }

  public void AddRange(IEnumerable<T> entities)
  {
    _dbSet.AddRange(entities);
  }

  public void Update(T entity)
  {
    _dbSet.Update(entity);
  }

  public void Delete(T entity)
  {
    _dbSet.Remove(entity);
  }

  public int SaveChanges()
  {
    return _context.SaveChanges();
  }

  public async Task<int> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync();
  }
}
