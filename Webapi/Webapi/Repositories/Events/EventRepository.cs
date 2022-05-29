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

  public async Task<IList<Event>> GetAllEventsByOwnerAsync(WebapiUser owner)
  {
    return await this._dbSet.Where(ev => ev.Owners.Contains(owner)).ToListAsync();
  }

  public async Task<Event?> GetEventAsync(string id)
  {
    return await this._dbSet.SingleOrDefaultAsync(entity => entity.Id == id);
  }

  public async Task AddEventAsync(Event ev)
  {
    await this._dbSet.AddAsync(ev);
    await this._context.SaveChangesAsync();
  }
}