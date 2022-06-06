namespace Webapi.Models.Events;

#pragma warning disable CS8618 // data transfer object
public class SignUpDto
{
  public int Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Username { get; set; }

  public string? AlsoKnownAs { get; set; }

  public SignUpStatus Status { get; set; }
}