namespace Webapi.Models.Comments;

public class Comment : BaseEntity
{
  public string Text { get; set; }

  public bool Edited { get; set; }

  public Comment() : base()
  {
    Text = string.Empty;
    Edited = false;
  }

  public Comment(CommentCreateDto commentCreateDto) : base()
  {
    Text = commentCreateDto.Text;
    Edited = false;
  }

  public void Update(CommentUpdateDto commentUpdateDto)
  {
    Text = commentUpdateDto.Text;
    Edited = true;
  }
}