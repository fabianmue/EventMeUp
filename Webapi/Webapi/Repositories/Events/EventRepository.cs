using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Webapi.DatabaseContext;
using Webapi.Models.Events;

namespace Webapi.Repositories.Events;

public class EventRepository : EntityRepository<Event>, IEventRepository
{
  public EventRepository(WebapiContext context) : base(context)
  {
  }

  public override Event? Find(Expression<Func<Event, bool>> expression)
  {
    return _dbSet
      .Include(ev => ev.Signups)
      .ThenInclude(signup => signup.Comments)
      .SingleOrDefault(expression);
  }

  public override async Task<Event?> FindAsync(Expression<Func<Event, bool>> expression)
  {
    return await _dbSet
      .Include(ev => ev.Signups)
      .ThenInclude(signup => signup.Comments)
      .SingleOrDefaultAsync(expression);
  }
}