﻿using ComputerGraphicsProject.Models;
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
            Complex c = new Complex(newtonFractalModel.realC, newtonFractalModel.imaginaryC);

            newtonFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
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

                return RedirectToAction("Vicek");
            }

            vicsekFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
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
            if (newtonFractalModelCtor.FractalBytesModel.FractalBytes == null)
            {
                newtonFractalModelCtor.FractalBytesModel.FractalBytes = new byte[0];
            }
            return File(newtonFractalModelCtor.FractalBytesModel.FractalBytes, "image/png");
        }
        [HttpGet]
        public IActionResult GetVicsecFractalImage()
        {
            vicsekFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            if (vicsekFractalModelCtor.FractalBytesModel.FractalBytes == null)
            {
                vicsekFractalModelCtor.FractalBytesModel.FractalBytes = new byte[0];
            }
            return File(vicsekFractalModelCtor.FractalBytesModel.FractalBytes, "image/png");
        }

    }
}

