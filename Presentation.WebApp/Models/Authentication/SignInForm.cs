using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models.Authentication;

public class SignInForm
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Your Password")]
    public string Password { get; set; } = null!;

    public string? ErrorMessage { get; set; }
}
