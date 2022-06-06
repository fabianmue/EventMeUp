using Microsoft.AspNetCore.Identity;
using Webapi.Models.Events;
using Webapi.Models.Identity;
using Webapi.Repositories.Events;

namespace Webapi.Services.Events;

public class EventService : IEventService
{
  private readonly IEventRepository _eventRepository;

  private readonly UserManager<WebapiUser> _userManager;

  public EventService(IEventRepository eventRepository, UserManager<WebapiUser> userManager)
  {
    this._eventRepository = eventRepository;
    this._userManager = userManager;
  }

  public async Task<Event?> AddEventSignUpAsync(string eventId, SignUpCreateDto signUpCreateDto)
  {
    Event? ev = await this._eventRepository.GetEventAsync(eventId);
    if (ev == null)
    {
      return null;
    }

    WebapiUser? user = null;
    if (signUpCreateDto.Email != null)
    {
      user = await this._userManager.FindByEmailAsync(signUpCreateDto.Email);
    }

    ev.SignUps.Add(new SignUp(signUpCreateDto, user));
    await this._eventRepository.SaveChangesAsync();
    return ev;
  }
}