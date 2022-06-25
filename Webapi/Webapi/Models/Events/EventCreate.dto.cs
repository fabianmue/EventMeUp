namespace Webapi.Models.Events;

#pragma warning disable CS8618 // data transfer object
public class EventCreateDto
{
  public string CreatedBy { get; set; }

  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public EventCategory? Category { get; set; }

  public string? Location { get; set; }
}