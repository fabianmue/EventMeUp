using Webapi.Models.Events;
using Webapi.Models.Identity;

namespace Webapi.Repositories.Events;

public interface IEventRepository
{
  Task<IList<Event>> GetAllEventsByOwnerAsync(WebapiUser owner);

  Task<IList<Event>> GetAllEventsBySingedUpUserAsync(WebapiUser owner);

  Task<Event?> GetEventAsync(string id);

  Task AddEventAsync(Event ev);

  Task AddEventRangeAsync(List<Event> events);

  Task<int> SaveChangesAsync();
}