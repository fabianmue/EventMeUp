using Webapi.Models.Comments;
using Webapi.Models.Events;
using Webapi.Models.Signups;

namespace Webapi.Services.Signups;

public interface ISignupService
{
  Task<Event?> AddSignupAsync(SignupCreateDto signupCreateDto);

  Task<Signup?> AddCommentAsync(CommentCreateDto commentCreateDto);
}