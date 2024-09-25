using Microsoft.AspNetCore.Mvc;
using Plant.AspNetCore;

namespace AdaClientX.Controllers;

public class HomeController : PlantController
{
    public IActionResult Index()
    {
        return View();
    }
}
