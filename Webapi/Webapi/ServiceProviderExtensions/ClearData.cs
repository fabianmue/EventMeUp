using Webapi.DatabaseContext;
using Webapi.Models.Events;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void ClearData(this IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<WebapiContext>();
    context.ClearDatabaseSet<Event>();
    context.SaveChanges();
  }

  private static void ClearDatabaseSet<TEntity>(this WebapiContext context) where TEntity : class
  {
    var dbSet = context.Set<TEntity>();
    dbSet.RemoveRange(dbSet);
  }
}