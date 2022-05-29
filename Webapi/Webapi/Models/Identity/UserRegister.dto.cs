using System.ComponentModel.DataAnnotations;

namespace Webapi.Models.Identity;

public class UserRegisterDto
{
  [Required]
  public string? Username { get; set; }

  [Required]
  public string? Email { get; set; }

  [Required]
  public string? Password { get; set; }
}