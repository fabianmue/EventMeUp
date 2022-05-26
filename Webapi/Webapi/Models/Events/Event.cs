using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shortid;
using shortid.Configuration;

namespace Webapi.Models.Events;

public class Event
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public string Id { get; set; }

  public string Title { get; set; }

  public string? Description { get; set; }

  public DateTime Start { get; set; }

  public DateTime? End { get; set; }

  public string? Where { get; set; }

  public string? Notes { get; set; }

  public Event()
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = default!;
  }

  public Event(string title)
  {
    this.Id = ShortId.Generate(ShortIdOptions);
    this.Title = title;
  }

  private static readonly GenerationOptions ShortIdOptions =
    new(useNumbers: true, useSpecialCharacters: false, length: 10);
}