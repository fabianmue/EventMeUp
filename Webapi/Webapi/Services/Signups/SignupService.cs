using Webapi.Models.Comments;
using Webapi.Models.Events;
using Webapi.Models.Signups;
using Webapi.Repositories.Events;
using Webapi.Repositories.Signups;

namespace Webapi.Services.Signups;

public class SignupService : ISignupService
{
  private readonly IEventRepository _eventRepository;

  private readonly ISignupRepository _signupRepository;

  public SignupService(IEventRepository eventRepository,
    ISignupRepository signupRepository)
  {
    _eventRepository = eventRepository;
    _signupRepository = signupRepository;
  }

  public async Task<Signup?> AddSignupAsync(string eventId, SignupCreateDto signupCreateDto)
  {
    Event? ev = _eventRepository
      .Find(ev => ev.Id == eventId);
    if (ev == null)
    {
      return null;
    }

    var signup = new Signup(signupCreateDto);
    ev.Signups.Add(signup);
    _eventRepository.Update(ev);
    await _eventRepository.SaveChangesAsync();
    return signup;
  }

  public async Task<Comment?> AddCommentAsync(string signupId, CommentCreateDto commentCreateDto)
  {
    Signup? signup = _signupRepository
      .Find(signup => signup.Id == signupId);
    if (signup == null)
    {
      return null;
    }

    var comment = new Comment(commentCreateDto);
    signup.Comments.Add(comment);
    _signupRepository.Update(signup);
    await _signupRepository.SaveChangesAsync();
    return comment;
  }
}