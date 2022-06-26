namespace Webapi.Models.Comments;

public class Comment : BaseEntity
{
  public string Text { get; set; }

  public Comment() : base()
  {
    this.Text = string.Empty;
  }

  public Comment(CommentCreateDto commentCreateDto) : base()
  {
    this.Text = commentCreateDto.Text;
  }

  public void Update(CommentUpdateDto commentUpdateDto)
  {
    this.Text = commentUpdateDto.Text;
  }
}