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

    List<WebapiUser> createdUsers = SeedWebapiUsers(userManager, mapper, UserRegisterDtos);
    List<SignUp> createdSignUps = SeedSignUps(context, SignUpCreateDtos, createdUsers);
    List<Event> _ = SeedEvents(context, EventCreateDtos, createdSignUps, createdUsers);
  }

  private static List<WebapiUser> SeedWebapiUsers(
    UserManager<WebapiUser> userManager,
    IMapper mapper,
    List<UserRegisterDto> userRegisterDtos)
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

  private static List<SignUp> SeedSignUps(
    WebapiContext context,
    List<SignUpCreateDto> signUpCreateDtos,
    List<WebapiUser> users
  )
  {
    var signUps = signUpCreateDtos
      .Select(signUpCreateDto =>
      {
        WebapiUser? matchingUser = signUpCreateDto.Email != null
          ? users.FirstOrDefault(user => user.Email == signUpCreateDto.Email)
          : null;
        return new SignUp(signUpCreateDto, matchingUser);
      })
      .ToList();
    context.SeedDbSet<SignUp>(signUps);
    return signUps;
  }

  private static List<Event> SeedEvents(
    WebapiContext context,
    List<EventCreateDto> eventCreateDtos,
    List<SignUp> signUps,
    List<WebapiUser> users)
  {
    var random = new Random();
    List<Event> events = eventCreateDtos
      .Select(eventCreateDto =>
      {
        WebapiUser owner = users.ElementAt(random.Next(users.Count));
        return new Event(eventCreateDto, owner, signUps);
      })
      .ToList();
    context.SeedDbSet<Event>(events);
    return events;
  }

  private static void SeedDbSet<TEntity>(this WebapiContext context, IEnumerable<TEntity> range) where TEntity : class
  {
    var dbSet = context.Set<TEntity>();
    if (dbSet.Any())
    {
      return;
    }

    dbSet.AddRange(range);
    context.SaveChanges();
  }

  private static readonly List<UserRegisterDto> UserRegisterDtos = new()
  {
    new UserRegisterDto {
      Email = "user@eventmeup.test",
      Username = "Username",
      Password = "EventMeUp123"
    }
  };

  private static readonly List<SignUpCreateDto> SignUpCreateDtos = new()
  {
    new SignUpCreateDto
    {
      Username = "Random person on the internet one",
      AlsoKnownAs = "the stranger",
      Status = SignUpStatus.Accepted
    },
    new SignUpCreateDto
    {
      Username = "Username",
      AlsoKnownAs = "the registered",
      Status = SignUpStatus.Accepted,
      Email = "user@eventmeup.test"
    }
  };

  private static readonly List<EventCreateDto> EventCreateDtos = new()
  {
    new EventCreateDto
    {
      Title = "Squash - blood sweat and tears (of joy!)",
      Description = "It's all fun and games until...",
      Start = new DateTime(2022, 5, 24, 12, 0, 0).ToUniversalTime(),
      Notes = "Bring your own racket or rent one",
      Location = "Airgate, Oerlikon"
    },
    new EventCreateDto
    {
      Title = "Squash - the sweet squashvenge",
      Description = "Fool me once, shame on me. Fool me twice, shame on - wait, what?",
      Start = new DateTime(2022, 5, 31, 18, 30, 0).ToUniversalTime(),
      End = new DateTime(2022, 5, 31, 19, 15, 0).ToUniversalTime(),
      Notes = "Bring your own racket",
      Location = "Vitis, Schlieren"
    }
  };
}