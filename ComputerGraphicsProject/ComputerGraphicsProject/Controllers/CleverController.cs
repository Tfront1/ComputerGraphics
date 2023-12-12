using Microsoft.AspNetCore.Mvc;

namespace ComputerGraphicsProject.Controllers
{
    public class CleverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
