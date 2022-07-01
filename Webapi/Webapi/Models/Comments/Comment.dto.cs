namespace Webapi.Models.Comments;

#pragma warning disable CS8618 // data transfer object
public class CommentDto : BaseDto
{
  public string Text { get; set; }

  public bool Edited { get; set; }
}