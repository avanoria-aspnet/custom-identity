using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models.Authentication;

public class SignInForm
{
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address", Prompt = "username@example.com")]
    public string Email { get; set; } = null!;


    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Your Password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }

    public string? ErrorMessage { get; set; }
}
