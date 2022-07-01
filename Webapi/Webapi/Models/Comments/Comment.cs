namespace Webapi.Models.Comments;

public class Comment : BaseEntity
{
  public string Text { get; set; }

  public bool Edited { get; set; }

  public Comment() : base()
  {
    this.Text = string.Empty;
    this.Edited = false;
  }

  public Comment(CommentCreateDto commentCreateDto) : base()
  {
    this.Text = commentCreateDto.Text;
    this.Edited = false;
  }

  public void Update(CommentUpdateDto commentUpdateDto)
  {
    this.Text = commentUpdateDto.Text;
    this.Edited = true;
  }
}