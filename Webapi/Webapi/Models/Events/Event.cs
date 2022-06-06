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

  public DateTime CreatedAt { get; set; }

  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Location { get; set; }

  public string? Notes { get; set; }

  public List<WebapiUser> Owners { get; set; }

  public List<SignUp> SignUps { get; set; }

  public Event()
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = string.Empty;
    this.Owners = new List<WebapiUser>();
    this.SignUps = new List<SignUp>();
  }

  public Event(EventCreateDto eventCreateDto, WebapiUser owner, List<SignUp>? signUps = null)
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = eventCreateDto.Title;
    this.Description = eventCreateDto.Description;
    this.Start = eventCreateDto.Start;
    this.End = eventCreateDto.End;
    this.Location = eventCreateDto.Location;
    this.Notes = eventCreateDto.Notes;
    this.Owners = new List<WebapiUser> { owner };
    this.SignUps = signUps ?? new List<SignUp>();
  }

  private static readonly GenerationOptions ShortIdOptions =
    new(useNumbers: true, useSpecialCharacters: false, length: 10);
}