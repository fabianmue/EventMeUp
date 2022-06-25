namespace Webapi.Models;

#pragma warning disable CS8618 // data transfer object
public class EditableDto : BaseDto
{
  public string CreatedBy { get; set; }
}