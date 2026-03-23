using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models.Authentication;

namespace Presentation.WebApp.Controllers;

public class AuthenticationController : Controller
{
    private const string SecretPassword = "BytMig123!";

    [HttpGet]
    public IActionResult SignIn(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public IActionResult SignIn(SignInForm form, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(form);

        if (form.Password != SecretPassword)
        {
            ModelState.AddModelError(nameof(form.ErrorMessage), "Incorrect Password");
            return View(form);
        }

        return View();
    }
}
