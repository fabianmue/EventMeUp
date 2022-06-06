using Webapi.Models.Events;

namespace Webapi.Services.Events;

public interface IEventService
{
  Task<Event?> AddEventSignUpAsync(string eventId, SignUpCreateDto signUpCreateDto);
}