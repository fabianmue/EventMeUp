using Webapi.Models.Identity;

namespace Webapi.Models.Events;

#pragma warning disable CS8618 // data transfer object
public class EventDto
{
  public string Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public EventCategory? Category { get; set; }

  public string? Location { get; set; }

  public string? Notes { get; set; }

  public List<SignUpDto> SignUps { get; set; }
}