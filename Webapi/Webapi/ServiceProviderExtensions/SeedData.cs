using Webapi.DatabaseContext;
using Webapi.Models.Events;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void SeedData(this IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<WebapiContext>();
    context.SeedDbSet<Event>(Events);
    context.SaveChanges();
  }

  private static void SeedDbSet<TEntity>(this WebapiContext context, IEnumerable<TEntity> range) where TEntity : class
  {
    var dbSet = context.Set<TEntity>();
    if (dbSet.Any())
    {
      return;
    }

    dbSet.AddRange(range);
  }

  private static readonly IList<Event> Events = new List<Event>
  {
    new Event("Squash - blood sweat and tears (of joy!)")
    {
      Description = "It's all fun and games until...",
      Start = new DateTime(2022, 5, 24, 12, 0, 0).ToUniversalTime()
    },
    new Event("Squash - the sweet squashvenge")
    {
      Description = "Fool me once, shame on me. Fool me twice, shame on - wait, what?",
      Start = new DateTime(2022, 5, 31, 12, 0, 0).ToUniversalTime()
    }
  };
}