using System.ComponentModel.DataAnnotations;

namespace Webapi.Models.Identity;

public class UserLoginDto
{
  [Required]
  public string? Email { get; set; }

  [Required]
  public string? Password { get; set; }
}