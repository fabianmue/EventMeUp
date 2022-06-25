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
    this._context = context;
    this._dbSet = this._context.Set<T>();
  }

  public virtual T? Find(Expression<Func<T, bool>> expression)
  {
    return this._dbSet.SingleOrDefault(expression);
  }

  public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
  {
    return await this._dbSet.SingleOrDefaultAsync(expression);
  }

  public void Add(T entity)
  {
    this._dbSet.Add(entity);
  }

  public void AddRange(IEnumerable<T> entities)
  {
    this._dbSet.AddRange(entities);
  }

  public void Update(T entity)
  {
    this._dbSet.Update(entity);
  }

  public void Delete(T entity)
  {
    this._dbSet.Remove(entity);
  }

  public int SaveChanges()
  {
    return this._context.SaveChanges();
  }

  public async Task<int> SaveChangesAsync()
  {
    return await this._context.SaveChangesAsync();
  }
}
