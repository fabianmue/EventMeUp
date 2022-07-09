using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Webapi.DatabaseContext;
using Webapi.Models.Signups;

namespace Webapi.Repositories.Signups;

public class SignupRepository : EntityRepository<Signup>, ISignupRepository
{
  public SignupRepository(WebapiContext context) : base(context)
  {
  }

  public override Signup? Find(Expression<Func<Signup, bool>> expression)
  {
    return _dbSet
      .Include(signup => signup.Comments)
      .SingleOrDefault(expression);
  }

  public override async Task<Signup?> FindAsync(Expression<Func<Signup, bool>> expression)
  {
    return await _dbSet
      .Include(signup => signup.Comments)
      .SingleOrDefaultAsync(expression);
  }
}