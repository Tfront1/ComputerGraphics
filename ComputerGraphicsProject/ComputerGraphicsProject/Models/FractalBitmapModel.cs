using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace ComputerGraphicsProject.Models
{
    public class FractalBitmapModel
    {

        public int Width { get; set; }
        public int Height { get; set; }

        public Bitmap Bitmap { get; set; }

        public FractalBitmapModel()
        {
            Width = 3000;
            Height = 3000;
        }
    }
}
