using Webapi.Models.Events;

namespace Webapi.DatabaseContext.WebapiContextExtensions;

public static partial class WebapiContextExtensions
{
  public static void Seed(this WebapiContext context)
  {
    context.SeedDbSet<Event>(Events);
    context.SaveChanges();
  }

  private static void SeedDbSet<TEntity>(this WebapiContext context, IEnumerable<TEntity> range) where TEntity : class
  {
    var dbSet = context.Set<TEntity>();
    if (!dbSet.Any())
    {
      dbSet.AddRange(range);
    }
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