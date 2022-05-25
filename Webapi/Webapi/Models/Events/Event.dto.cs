namespace Webapi.Models.Events;

#pragma warning disable CS8618 // data transfer object
public class EventDto
{
  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Where { get; set; }

  public string? Notes { get; set; }
}