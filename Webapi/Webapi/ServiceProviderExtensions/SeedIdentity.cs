using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Webapi.Models.Identity;

namespace Webapi.ServiceProviderExtensions;

public static partial class ServiceProviderExtensions
{
  public static void SeedIdentity(this IServiceProvider serviceProvider)
  {
    var userManager = serviceProvider.GetRequiredService<UserManager<WebapiUser>>();
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    userManager.SeedUsers(mapper, UserRegisterDtos);
  }

  private static void SeedUsers(
    this UserManager<WebapiUser> userManager,
    IMapper mapper,
    IEnumerable<UserRegisterDto> userRegisterDtos)
  {
    if (userManager.Users.Any())
    {
      return;
    }

    var results = userRegisterDtos
      .Select(userRegisterDto =>
      {
        var user = mapper.Map<WebapiUser>(userRegisterDto);
        return userManager.CreateAsync(user, userRegisterDto.Password).Result;
      });

    if (results.Where(result => !result.Succeeded).Any())
    {
      throw new Exception("Error seeding identity");
    }
  }

  private static readonly IList<UserRegisterDto> UserRegisterDtos = new List<UserRegisterDto>
  {
    new UserRegisterDto {
      Email = "user@eventmeup.test",
      Username = "user",
      Password = "EventMeUp123"
    }
  };
}