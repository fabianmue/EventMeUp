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
    this.Title = string.Empty;
    this.Signups = new List<Signup>();
  }

  public Event(EventCreateDto eventCreateDto) : base()
  {
    this.CreatedBy = eventCreateDto.CreatedBy;
    this.Title = eventCreateDto.Title;
    this.Description = eventCreateDto.Description;
    this.Start = eventCreateDto.Start;
    this.End = eventCreateDto.End;
    this.Category = eventCreateDto.Category;
    this.Location = eventCreateDto.Location;
    this.Signups = new List<Signup>();
  }

  public void Update(EventUpdateDto eventUpdateDto)
  {
    this.Title = eventUpdateDto.Title;
    this.Description = eventUpdateDto.Description;
    this.Start = eventUpdateDto.Start;
    this.End = eventUpdateDto.End;
    this.Category = eventUpdateDto.Category;
    this.Location = eventUpdateDto.Location;
  }
}