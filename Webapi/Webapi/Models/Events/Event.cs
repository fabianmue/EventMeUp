using Webapi.Models.Signups;

namespace Webapi.Models.Events;

public class Event : EditableEntity
{
  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public EventCategory? Category { get; set; }

  public string? Location { get; set; }

  public List<Signup> Signups { get; set; }

  public Event() : base()
  {
    Title = string.Empty;
    Signups = new List<Signup>();
  }

  public Event(EventCreateDto eventCreateDto) : base()
  {
    CreatedBy = eventCreateDto.CreatedBy;
    Title = eventCreateDto.Title;
    Description = eventCreateDto.Description;
    Start = eventCreateDto.Start;
    End = eventCreateDto.End;
    Category = eventCreateDto.Category;
    Location = eventCreateDto.Location;
    Signups = new List<Signup>();
  }

  public void Update(EventUpdateDto eventUpdateDto)
  {
    Title = eventUpdateDto.Title;
    Description = eventUpdateDto.Description;
    Start = eventUpdateDto.Start;
    End = eventUpdateDto.End;
    Category = eventUpdateDto.Category;
    Location = eventUpdateDto.Location;
  }
}