using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using ComputerGraphicsProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsProject.Controllers
{
    public class ColorSchemesController : Controller
    {
        private readonly ICmykService cmykService;
        private readonly IHslService hslService;
        private CmykModel cmykModel = new CmykModel();
        private HslModel hslModel = new HslModel();
        public ColorSchemesController(ICmykService cmykService, IHslService hslService)
        {
            this.cmykService = cmykService;
            this.hslService = hslService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Hsl()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cmyk()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cmyk(CmykModel cmykModel)
        {
            TryValidateModel(cmykModel);
            if (!ModelState.IsValid)
            {

                return View();
            }
            cmykModel.colorBytesModel = ColorBytesModel.GetInstance();
            cmykModel.colorBytesModel.Bytes = cmykService.GenerateCmyk(cmykModel);
            return View(cmykModel);
        }

        [HttpGet]
        public IActionResult GetCmykImage()
        {
            cmykModel.colorBytesModel = ColorBytesModel.GetInstance();
            if (cmykModel.colorBytesModel.Bytes == null)
            {
                cmykModel.colorBytesModel.Bytes = getWhiteImg();
            }
            return File(cmykModel.colorBytesModel.Bytes, "image/jpeg");
        }

        [HttpPost]
        public IActionResult hSL(HslModel hslModel)
        {
            TryValidateModel(hslModel);
            if (!ModelState.IsValid)
            {
                return View();
            }
            hslModel.colorBytesModel = ColorBytesModel.GetInstance();
            hslModel.colorBytesModel.Bytes = hslService.GenerateHsl(hslModel);
            return View(hslModel);
        }

        [HttpGet]
        public IActionResult GetHslImage()
        {
            hslModel.colorBytesModel = ColorBytesModel.GetInstance();
            if (hslModel.colorBytesModel.Bytes == null)
            {
                hslModel.colorBytesModel.Bytes = getWhiteImg();
            }
            return File(hslModel.colorBytesModel.Bytes, "image/jpeg");
        }

        [NonAction]
        public byte[] getWhiteImg()
        {
            byte[] whiteImageBytes;
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                bmp.SetPixel(0, 0, System.Drawing.Color.White);
                bmp.Save(ms, ImageFormat.Png);
                whiteImageBytes = ms.ToArray();
            }
            return whiteImageBytes;
        }
    }
}
