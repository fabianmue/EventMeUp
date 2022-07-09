using Webapi.Models.Comments;

namespace Webapi.Models.Signups;

public class Signup : EditableEntity
{
  public SignupStatus Status { get; set; }

  public List<Comment> Comments { get; set; }

  public Signup() : base()
  {
    Comments = new List<Comment>();
  }

  public Signup(SignupCreateDto signupCreateDto) : base()
  {
    CreatedBy = signupCreateDto.CreatedBy;
    Status = signupCreateDto.Status;
    Comments = new List<Comment>();
  }

  public void Update(SignupUpdateDto signupUpdateDto)
  {
    Status = signupUpdateDto.Status;
  }
}