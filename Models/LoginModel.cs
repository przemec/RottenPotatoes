using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace rottenpotatoes.Models
{
  [AllowAnonymous]
  public class LoginModel
  {
    [Required(ErrorMessage ="Please type nickname!")]
    public string Nick { get; set; }
    
    [Required(ErrorMessage ="Please type password!")]
    [UIHint("Password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; } = "/";
  }
}
