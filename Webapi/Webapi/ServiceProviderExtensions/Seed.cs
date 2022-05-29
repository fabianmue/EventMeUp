using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Webapi.DatabaseContext;
using Webapi.Models.Events;
using Webapi.Models.Identity;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void Seed(this IServiceProvider serviceProvider)
  {
    var context = serviceProvider.GetRequiredService<WebapiContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<WebapiUser>>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();

    IList<WebapiUser> createdUsers = userManager.SeedWebapiUsers(mapper, UserRegisterDtos);
    IList<Event> events = EventDtos
      .Select(eventDto => new Event(eventDto, createdUsers.ElementAt(0)))
      .ToList();
    context.SeedDbSet<Event>(events);
    context.SaveChanges();
  }

  private static IList<WebapiUser> SeedWebapiUsers(
    this UserManager<WebapiUser> userManager,
    IMapper mapper,
    IEnumerable<UserRegisterDto> userRegisterDtos)
  {
    if (userManager.Users.Any())
    {
      return new List<WebapiUser>();
    }

    // FIXME (fabianmue): find better way to asynchronously create users with usermanager
    return userRegisterDtos
      .Select(userRegisterDto =>
      {
        var user = mapper.Map<WebapiUser>(userRegisterDto);
        var createResult = userManager.CreateAsync(user, userRegisterDto.Password).Result;
        return (CreateResult: createResult, User: user);
      })
      .Where(userWithResult => userWithResult.CreateResult.Succeeded)
      .Select(userWithResult => userWithResult.User)
      .ToList();
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

  private static readonly IList<UserRegisterDto> UserRegisterDtos = new List<UserRegisterDto>
  {
    new UserRegisterDto {
      Email = "user@eventmeup.test",
      Username = "user",
      Password = "EventMeUp123"
    }
  };

  private static readonly IList<EventDto> EventDtos = new List<EventDto>
  {
    new EventDto
    {
      Title = "Squash - blood sweat and tears (of joy!)",
      Description = "It's all fun and games until...",
      Start = new DateTime(2022, 5, 24, 12, 0, 0).ToUniversalTime(),
      Notes = "Bring your own racket or rent one",
      Location = "Airgate, Oerlikon"
    },
    new EventDto
    {
      Title = "Squash - the sweet squashvenge",
      Description = "Fool me once, shame on me. Fool me twice, shame on - wait, what?",
      Start = new DateTime(2022, 5, 31, 12, 0, 0).ToUniversalTime(),
      Notes = "Bring your own racket or rent one",
      Location = "Vitis, Schlieren"
    }
  };
}