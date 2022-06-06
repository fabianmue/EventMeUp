namespace Webapi.Models.Events;

#pragma warning disable CS8618 // data transfer object
public class EventCreateDto
{
  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Location { get; set; }

  public string? Notes { get; set; }

  public List<SignUpCreateDto>? SignUps { get; set; }
}