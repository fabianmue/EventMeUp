using System.ComponentModel.DataAnnotations.Schema;
using shortid;

namespace Webapi.Models;

public class EditableEntity : BaseEntity
{
  public string CreatedBy { get; set; }

  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public string EditToken { get; set; }

  public EditableEntity() : base()
  {
    CreatedBy = string.Empty;
    EditToken = ShortId.Generate(ShortIdOptions);
  }
}