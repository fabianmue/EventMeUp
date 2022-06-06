using Webapi.Models.Identity;

namespace Webapi.Models.Events;

public class SignUp
{
  public int Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Username { get; set; }

  public string? AlsoKnownAs { get; set; }

  public SignUpStatus Status { get; set; }

  public WebapiUser? User { get; set; }

  public List<Event> Events { get; set; }

  public SignUp()
  {
    this.Username = string.Empty;
    this.Events = new List<Event>();
  }

  public SignUp(SignUpCreateDto signUpCreateDto, WebapiUser? user = null)
  {
    this.Username = signUpCreateDto.Username;
    this.AlsoKnownAs = signUpCreateDto.AlsoKnownAs;
    this.Status = signUpCreateDto.Status;
    this.User = user;
    this.Events = new List<Event>();
  }
}