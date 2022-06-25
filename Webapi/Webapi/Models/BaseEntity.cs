using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using shortid;
using shortid.Configuration;

namespace Webapi.Models;

public class BaseEntity
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public string Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public BaseEntity()
  {
    this.Id = ShortId.Generate(ShortIdOptions);
  }

  protected static readonly GenerationOptions ShortIdOptions =
    new(useNumbers: true, useSpecialCharacters: false, length: 10);
}