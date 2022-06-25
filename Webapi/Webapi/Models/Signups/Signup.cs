using Webapi.Models.Comments;

namespace Webapi.Models.Signups;

public class Signup : EditableEntity
{
  public SignupStatus Status { get; set; }

  public List<Comment> Comments { get; set; }

  public Signup() : base()
  {
    this.Comments = new List<Comment>();
  }

  public Signup(SignupCreateDto signupCreateDto) : base()
  {
    this.CreatedBy = signupCreateDto.CreatedBy;
    this.Status = signupCreateDto.Status;
    this.Comments = new List<Comment>();
  }
}