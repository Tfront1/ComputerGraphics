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

            newtonFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            newtonFractalModelCtor.FractalBytesModel.FractalBytes = new NewtonFractalService().
                GenerateFractal(newtonFractalModel.FractalBitmapModel.Width,
                newtonFractalModel.FractalBitmapModel.Height,
                newtonFractalModel.MaxIterations, newtonFractalModel.Threshold,
                newtonFractalModel.Exponent,
                c);
            return View(newtonFractalModelCtor);
        }
        [HttpGet]
        public IActionResult GetFractalImage()
        {
            newtonFractalModelCtor.FractalBytesModel = FractalBytesModel.GetInstance();
            if (newtonFractalModelCtor.FractalBytesModel.FractalBytes == null)
            {
                newtonFractalModelCtor.FractalBytesModel.FractalBytes = new byte[0];
            }
            return File(newtonFractalModelCtor.FractalBytesModel.FractalBytes, "image/png");
        }

    }
}

