using ComputerGraphicsProject.Models;
using ComputerGraphicsProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Numerics;

namespace ComputerGraphicsProject.Controllers
{
    public class FractalsController : Controller
    {
        NewtonFractalModel newtonFractalModel = new NewtonFractalModel();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Vicek()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Newton()
        {
            return View(newtonFractalModel);
        }

        [HttpPost]
        public IActionResult Newton(NewtonFractalModel newtonFractalModel)
        {
            Complex c = new Complex(newtonFractalModel.realC, newtonFractalModel.imaginaryC);
            newtonFractalModel.FractalBitmapModel.Bitmap = new NewtonFractalService().
                GenerateFractal(newtonFractalModel.FractalBitmapModel.Width, 
                newtonFractalModel.FractalBitmapModel.Height,
                newtonFractalModel.MaxIterations, newtonFractalModel.Threshold, 
                newtonFractalModel.Exponent, 
                c);
            //newtonFractalModel.FractalBitmapModel.Bitmap.Save("output.png");
            return View(newtonFractalModel);
        }
    }
}
