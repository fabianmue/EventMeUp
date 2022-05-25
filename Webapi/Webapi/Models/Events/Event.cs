using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Models.Events;

public class Event
{
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public string Id { get; set; } = default!;

  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Where { get; set; }

  public string? Notes { get; set; }

  public Event()
  {
    this.Title = default!;
  }

  public Event(string title)
  {
    this.Title = title;
  }
}