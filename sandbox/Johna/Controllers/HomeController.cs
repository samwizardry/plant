using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace Johna.Controllers;

public class HomeController : PlantController
{
    public IActionResult Index()
    {
        return View();
    }
}
