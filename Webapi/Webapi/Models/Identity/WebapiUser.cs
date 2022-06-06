using Microsoft.AspNetCore.Identity;
using Webapi.Models.Events;

namespace Webapi.Models.Identity;

public class WebapiUser : IdentityUser
{
  public List<Event> OwnedEvents { get; set; } = new List<Event>();
}