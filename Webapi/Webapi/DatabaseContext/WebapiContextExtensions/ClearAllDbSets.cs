using Webapi.Models.Events;

namespace Webapi.DatabaseContext.WebapiContextExtensions;

public static partial class WebapiContextExtensions
{
  public static void ClearAllDbSets(this WebapiContext context)
  {
    context.ClearDbSet<Event>();
    context.SaveChanges();
  }

  private static void ClearDbSet<TEntity>(this WebapiContext context) where TEntity : class
  {
    var dbSet = context.Set<TEntity>();
    dbSet.RemoveRange(dbSet);
  }
}