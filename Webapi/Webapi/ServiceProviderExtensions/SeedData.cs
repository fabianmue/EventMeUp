using Microsoft.AspNetCore.Identity;
using Webapi.DatabaseContext;
using Webapi.Models.Events;
using Webapi.Models.Identity;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void SeedData(this IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<WebapiContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<WebapiUser>>();
    var user = userManager.FindByEmailAsync("user@eventmeup.test").Result;
    context.SeedDbSet<Event>(AddEventOwner(user, Events));
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

  private static IList<Event> AddEventOwner(WebapiUser user, IList<Event> events)
  {
    return events
      .Select(ev =>
      {
        if (user != null)
        {
          ev.Owners.Add(user);
        }
        return ev;
      })
      .ToList();
  }

  private static readonly IList<Event> Events = new List<Event>
  {
    new Event("Squash - blood sweat and tears (of joy!)")
    {
      Description = "It's all fun and games until...",
      Start = new DateTime(2022, 5, 24, 12, 0, 0).ToUniversalTime(),
      Notes = "Bring your own racket or rent one",
      Location = "Airgate, Oerlikon"
    },
    new Event("Squash - the sweet squashvenge")
    {
      Description = "Fool me once, shame on me. Fool me twice, shame on - wait, what?",
      Start = new DateTime(2022, 5, 31, 12, 0, 0).ToUniversalTime(),
      Notes = "Bring your own racket or rent one",
      Location = "Vitis, Schlieren"
    }
  };
}