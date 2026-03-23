using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApp.Models.Authentication;
using System.Security.Claims;

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
    public async Task<IActionResult> SignIn(SignInForm form, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(form);

        if (form.Password != SecretPassword)
        {
            ModelState.AddModelError(nameof(form.ErrorMessage), "Incorrect Password");
            return View(form);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "hanmat"),
            new(ClaimTypes.Email, "hans@domain.com"),
            new(ClaimTypes.NameIdentifier, "16508e8d-39fa-494b-8451-b2321fcba747"),
            new(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            authProperties
        );

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Account");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public new async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("SignIn", "Authentication");
    }
}
