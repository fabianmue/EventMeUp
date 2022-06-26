using Webapi.Models.Comments;
using Webapi.Models.Signups;

namespace Webapi.Services.Signups;

public interface ISignupService
{
  Task<Signup?> AddSignupAsync(string eventId, SignupCreateDto signupCreateDto);

  Task<Comment?> AddCommentAsync(string signupId, CommentCreateDto commentCreateDto);
}