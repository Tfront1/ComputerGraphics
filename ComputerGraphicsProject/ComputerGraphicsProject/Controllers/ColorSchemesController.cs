using Microsoft.AspNetCore.Mvc;

namespace ComputerGraphicsProject.Controllers
{
    public class ColorSchemesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
