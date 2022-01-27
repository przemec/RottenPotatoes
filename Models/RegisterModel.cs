using System.ComponentModel.DataAnnotations;

namespace rottenpotatoes.Models
{
  public class RegisterModel
  {
    [Required(ErrorMessage = "Enter Your nickname!")]
    [RegularExpression("^[0-9a-zA-Z]+$", ErrorMessage = "Nickname can contain only numbers and letters")]
    [StringLength(15, ErrorMessage = "Nickname must contain between 3 and 15 characters!", MinimumLength = 3)]
    public string Nick { get; set; }

    [Required(ErrorMessage = "Enter the password!")]
    [StringLength(20, ErrorMessage = "Password must contain between 8 and 20 characters!", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Retype the password!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match!")]
    public string ConfirmPassword { get; set; }

    public string ReturnUrl { get; set; }
  }
}
