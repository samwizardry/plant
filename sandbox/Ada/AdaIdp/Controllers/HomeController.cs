using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace AdaIdp.Controllers;

public class HomeController : PlantController
{
    public IActionResult Index()
    {
        return View();
    }
}
