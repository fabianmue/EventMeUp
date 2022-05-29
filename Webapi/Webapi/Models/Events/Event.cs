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

  public IList<WebapiUser> Owners { get; set; } = new List<WebapiUser>();

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Location { get; set; }

  public string? Notes { get; set; }

  public Event()
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = default!;
  }

  public Event(EventDto eventDto, WebapiUser user)
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = eventDto.Title;
    this.Owners = new List<WebapiUser> { user };
    this.Description = eventDto.Description;
    this.Start = eventDto.Start;
    this.End = eventDto.End;
    this.Location = eventDto.Location;
    this.Notes = eventDto.Notes;
  }

  private static readonly GenerationOptions ShortIdOptions =
    new(useNumbers: true, useSpecialCharacters: false, length: 10);
}