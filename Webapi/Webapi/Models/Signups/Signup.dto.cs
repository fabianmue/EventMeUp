using Webapi.Models.Comments;

namespace Webapi.Models.Signups;

#pragma warning disable CS8618 // data transfer object
public class SignupDto : EditableDto
{
  public SignupStatus Status { get; set; }

  public List<CommentDto> Comments { get; set; }
}