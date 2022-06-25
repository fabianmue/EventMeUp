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

  public async Task<Event?> AddSignupAsync(SignupCreateDto signupCreateDto)
  {
    Event? ev = this._eventRepository
      .Find(ev => ev.Id == signupCreateDto.EventId);
    if (ev == null)
    {
      return null;
    }

    ev.Signups.Add(new Signup(signupCreateDto));
    this._eventRepository.Update(ev);
    await this._eventRepository.SaveChangesAsync();
    return ev;
  }

  public async Task<Signup?> AddCommentAsync(CommentCreateDto commentCreateDto)
  {
    Signup? signup = this._signupRepository
      .Find(signup => signup.Id == commentCreateDto.SignupId);
    if (signup == null)
    {
      return null;
    }

    signup.Comments.Add(new Comment(commentCreateDto));
    this._signupRepository.Update(signup);
    await this._signupRepository.SaveChangesAsync();
    return signup;
  }
}