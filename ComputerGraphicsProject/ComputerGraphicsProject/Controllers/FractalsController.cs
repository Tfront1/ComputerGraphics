using ComputerGraphicsProject.Models;
using ComputerGraphicsProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using ComputerGraphicsProject.Interfaces;

namespace ComputerGraphicsProject.Controllers
{
    enum Fractals
    {
        Newton = 1,
        Vicsek
    }
    public class FractalsController : Controller
    {
        NewtonFractalModel newtonFractalModelCtor = new NewtonFractalModel();
        VicsekFractalModel vicsekFractalModelCtor = new VicsekFractalModel();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
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
            TryValidateModel(newtonFractalModel);
            if (!ModelState.IsValid)
            {

                return View();
            }
            Complex c = new Complex(newtonFractalModel.RealC, newtonFractalModel.ImaginaryC);

            newtonFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            newtonFractalModelCtor.FractalBytesModel.LastGeneratedFractal = (int)Fractals.Newton;
            newtonFractalModelCtor.FractalBytesModel.FractalBytes = new NewtonFractalService().
                GenerateFractal(newtonFractalModel.FractalBitmapModel.Width,
                newtonFractalModel.FractalBitmapModel.Height,
                newtonFractalModel.MaxIterations, newtonFractalModel.Threshold,
                newtonFractalModel.Exponent,
                c);
            return View(newtonFractalModelCtor);
        }

        [HttpPost]
        public IActionResult Vicek(VicsekFractalModel vicsekFractalModel)
        {
            TryValidateModel(vicsekFractalModel);
            if (!ModelState.IsValid)
            {

                return View();
            }

            vicsekFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            vicsekFractalModelCtor.FractalBytesModel.LastGeneratedFractal = (int)Fractals.Vicsek;
            vicsekFractalModelCtor.FractalBytesModel.FractalBytes = new VicsecFractalService().
                GenerateFractal(vicsekFractalModel.FractalBitmapModel.Width/3,
                vicsekFractalModel.FractalBitmapModel.Height/3, 
                vicsekFractalModel.SideLength,
                vicsekFractalModel.IterationsCount);
            return View(vicsekFractalModelCtor);
        }
        [HttpGet]
        public IActionResult GetNewtonFractalImage()
        {
            newtonFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            if (newtonFractalModelCtor.FractalBytesModel.FractalBytes == null || 
                newtonFractalModelCtor.FractalBytesModel.LastGeneratedFractal == (int)Fractals.Vicsek)
            {
                newtonFractalModelCtor.FractalBytesModel.FractalBytes = getWhiteImg();
            }
            return File(newtonFractalModelCtor.FractalBytesModel.FractalBytes, "image/png");
        }
        [HttpGet]
        public IActionResult GetVicsecFractalImage()
        {
            vicsekFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            if (vicsekFractalModelCtor.FractalBytesModel.FractalBytes == null ||
                 vicsekFractalModelCtor.FractalBytesModel.LastGeneratedFractal == (int)Fractals.Newton)
            {
                vicsekFractalModelCtor.FractalBytesModel.FractalBytes = getWhiteImg();
            }
            return File(vicsekFractalModelCtor.FractalBytesModel.FractalBytes, "image/png");
        }

        [NonAction]
        public byte[] getWhiteImg() {
            byte[] whiteImageBytes;
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                bmp.SetPixel(0, 0, Color.White);
                bmp.Save(ms, ImageFormat.Png);
                whiteImageBytes = ms.ToArray();
            }
            return whiteImageBytes;
        }
    }
}

