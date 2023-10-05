using ComputerGraphicsProject.Models;
using ComputerGraphicsProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using ComputerGraphicsProject.Interfaces;

namespace ComputerGraphicsProject.Controllers
{
    public class FractalsController : Controller
    {
        NewtonFractalModel newtonFractalModelCtor = new NewtonFractalModel();
        FractalBytesModel fractalBytesModel;
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
            return View(newtonFractalModelCtor);
        }

        [HttpPost]
        public IActionResult Newton(NewtonFractalModel newtonFractalModel)
        {
            Complex c = new Complex(newtonFractalModel.realC, newtonFractalModel.imaginaryC);
            newtonFractalModelCtor.FractalBitmapModel.Bitmap = new NewtonFractalService().
                GenerateFractal(newtonFractalModel.FractalBitmapModel.Width, 
                newtonFractalModel.FractalBitmapModel.Height,
                newtonFractalModel.MaxIterations, newtonFractalModel.Threshold, 
                newtonFractalModel.Exponent, 
                c);
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the Bitmap as PNG to the MemoryStream.
                newtonFractalModelCtor.FractalBitmapModel.Bitmap.Save(ms, ImageFormat.Png);

                //fractalGeneratorService.setFractalBytes(ms.ToArray());
                fractalBytesModel = FractalBytesModel.GetInstance();
                fractalBytesModel.FractalBytes = ms.ToArray();

                return View(newtonFractalModelCtor);
            }
        }
        [HttpGet]
        public IActionResult GetFractalImage()
        {
            fractalBytesModel = FractalBytesModel.GetInstance();
            if (fractalBytesModel.FractalBytes == null)
            {
                fractalBytesModel.FractalBytes = new byte[0];
            }
            return File(fractalBytesModel.FractalBytes, "image/png");
        }
    }
}
