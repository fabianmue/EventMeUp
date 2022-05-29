using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shortid;
using shortid.Configuration;
using Webapi.Models.Identity;

namespace Webapi.Models.Events;

public class Event
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public string Id { get; set; }

  public string Title { get; set; }

  public DateTime CreatedAt { get; set; }

  public WebapiUser Owner { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Location { get; set; }

  public string? Notes { get; set; }

  public Event()
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = string.Empty;
    this.Owner = default!;
  }

  public Event(EventDto eventDto, WebapiUser owner)
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = eventDto.Title;
    this.Owner = owner;
    this.Description = eventDto.Description;
    this.Start = eventDto.Start;
    this.End = eventDto.End;
    this.Location = eventDto.Location;
    this.Notes = eventDto.Notes;
  }

  private static readonly GenerationOptions ShortIdOptions =
    new(useNumbers: true, useSpecialCharacters: false, length: 10);
}