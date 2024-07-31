using Microsoft.AspNetCore.Mvc;

namespace Johna.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
