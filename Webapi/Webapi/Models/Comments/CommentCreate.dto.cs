namespace Webapi.Models.Comments;

#pragma warning disable CS8618 // data transfer object
public class CommentCreateDto
{
  public string Text { get; set; }

  public string SignupId { get; set; }
}