namespace Webapi.Models;

#pragma warning disable CS8618 // data transfer object
public class BaseDto
{
  public string Id { get; set; }

  public DateTime CreatedAt { get; set; }
}