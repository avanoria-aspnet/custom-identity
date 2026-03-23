using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Areas.Admin.Controllers;

[Authorize(Roles = "Admin, Employee")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
