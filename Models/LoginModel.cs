using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace rottenpotatoes.Models
{
  [AllowAnonymous]
  public class LoginModel
  {
    [Required(ErrorMessage ="Proszę podać nick!")]
    public string Nick { get; set; }
    
    [Required(ErrorMessage ="Proszę podać hasło!")]
    [UIHint("password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; } = "/";
  }
}
