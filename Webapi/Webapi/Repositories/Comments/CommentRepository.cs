using Webapi.DatabaseContext;
using Webapi.Models.Comments;

namespace Webapi.Repositories.Comments;

public class CommentRepository : EntityRepository<Comment>, ICommentRepository
{
  public CommentRepository(WebapiContext context) : base(context)
  {
  }
}