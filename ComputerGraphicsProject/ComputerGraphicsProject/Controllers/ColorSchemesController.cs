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
        private CmykModel cmykModel = new CmykModel();
        public ColorSchemesController(ICmykService cmykService)
        {
            this.cmykService = cmykService;
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
            cmykModel.CmykBytesModel = CmykBytesModel.GetInstance();
            cmykModel.CmykBytesModel.Bytes = cmykService.GenerateCmyk(cmykModel);
            return View(cmykModel);
        }

        [HttpGet]
        public IActionResult GetCmykImage()
        {
            cmykModel.CmykBytesModel = CmykBytesModel.GetInstance();
            if (cmykModel.CmykBytesModel.Bytes == null)
            {
                cmykModel.CmykBytesModel.Bytes = getWhiteImg();
            }
            return File(cmykModel.CmykBytesModel.Bytes, "image/jpeg");
        }

        [NonAction]
        public byte[] getWhiteImg()
        {
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
