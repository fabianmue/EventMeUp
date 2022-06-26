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
    this._eventRepository = eventRepository;
    this._signupRepository = signupRepository;
  }

  public async Task<Signup?> AddSignupAsync(string eventId, SignupCreateDto signupCreateDto)
  {
    Event? ev = this._eventRepository
      .Find(ev => ev.Id == eventId);
    if (ev == null)
    {
      return null;
    }

    var signup = new Signup(signupCreateDto);
    ev.Signups.Add(signup);
    this._eventRepository.Update(ev);
    await this._eventRepository.SaveChangesAsync();
    return signup;
  }

  public async Task<Comment?> AddCommentAsync(string signupId, CommentCreateDto commentCreateDto)
  {
    Signup? signup = this._signupRepository
      .Find(signup => signup.Id == signupId);
    if (signup == null)
    {
      return null;
    }

    var comment = new Comment(commentCreateDto);
    signup.Comments.Add(comment);
    this._signupRepository.Update(signup);
    await this._signupRepository.SaveChangesAsync();
    return comment;
  }
}