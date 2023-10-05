using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ComputerGraphicsProject.Models
{
    public class FractalBitmapModel
    {

        public int Width { get; set; }
        public int Height { get; set; }

        public Bitmap Bitmap { get; set; }

        public FractalBitmapModel()
        {
            Width = 500;
            Height = 500;
        }
    }
}
