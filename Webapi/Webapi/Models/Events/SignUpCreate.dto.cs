namespace Webapi.Models.Events;

#pragma warning disable CS8618 // data transfer object
public class SignUpCreateDto
{
  public string Username { get; set; }

  public string? AlsoKnownAs { get; set; }

  public SignUpStatus Status { get; set; }

  public string? Email { get; set; }
}