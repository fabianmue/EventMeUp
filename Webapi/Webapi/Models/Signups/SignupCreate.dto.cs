namespace Webapi.Models.Signups;

#pragma warning disable CS8618 // data transfer object
public class SignupCreateDto
{
  public string CreatedBy { get; set; }

  public SignupStatus Status { get; set; }

  public string EventId { get; set; }
}