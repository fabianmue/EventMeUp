using Microsoft.EntityFrameworkCore;
using Webapi.DatabaseContext;
using Webapi.Models.Events;
using Webapi.Models.Identity;

namespace Webapi.Repositories.Events;

public class EventRepository : IEventRepository
{
  private readonly WebapiContext _context;

  private readonly DbSet<Event> _dbSet;

  public EventRepository(WebapiContext context)
  {
    this._context = context;
    this._dbSet = this._context.Set<Event>();
  }

  public async Task<IList<Event>> GetAllEventsByOwnerAsync(WebapiUser user)
  {
    return await this._dbSet
      .Where(ev => ev.Owners.Contains(user))
      .Include(ev => ev.SignUps)
      .Include(ev => ev.Owners)
      .ToListAsync();
  }

  public async Task<IList<Event>> GetAllEventsBySingedUpUserAsync(WebapiUser user)
  {
    return await this._dbSet
      .Where(ev =>
        ev.SignUps
          .Where(signUp => signUp.User != null)
          .Select(signUp => signUp.User)
          .Contains(user))
      .Include(ev => ev.SignUps)
      .Include(ev => ev.Owners)
      .ToListAsync();
  }

  public async Task<Event?> GetEventAsync(string id)
  {
    return await this._dbSet
      .Include(ev => ev.SignUps)
      .Include(ev => ev.Owners)
      .SingleOrDefaultAsync(entity => entity.Id == id);
  }

  public async Task AddEventAsync(Event ev)
  {
    await this._dbSet.AddAsync(ev);
    await this._context.SaveChangesAsync();
  }

  public async Task AddEventRangeAsync(List<Event> events)
  {
    await this._dbSet.AddRangeAsync(events);
    await this._context.SaveChangesAsync();
  }

  public async Task<int> SaveChangesAsync()
  {
    return await this._context.SaveChangesAsync();
  }
}