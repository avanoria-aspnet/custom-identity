using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Areas.Member.Controllers;

[Authorize]
public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
