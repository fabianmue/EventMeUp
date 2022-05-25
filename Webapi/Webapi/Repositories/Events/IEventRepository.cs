using Webapi.Models.Events;

namespace Webapi.Repositories.Events;

public interface IEventRepository
{
  Task<IList<Event>> GetAllEventsAsync();

  Task<Event?> GetEventAsync(string id);

  Task AddEventAsync(Event ev);
}