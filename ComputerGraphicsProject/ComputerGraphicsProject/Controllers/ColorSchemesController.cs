using Microsoft.AspNetCore.Mvc;

namespace ComputerGraphicsProject.Controllers
{
    public class ColorSchemesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Hsl()
        {
            return View();
        }
        public IActionResult Cmyk()
        {
            return View();
        }
    }
}
