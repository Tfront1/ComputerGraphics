using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComputerGraphicsProject.Models
{
    public class VicsekFractalModel
    {
        public FractalBitmapModel FractalBitmapModel { get; set; }

        public FractalBytesModel FractalBytesModel { get; set; }

        [Range(0, 1600)]
        public int SideLength { get; set; }
        [Range(1, 6)]
        public int IterationsCount { get; set; }

        public VicsekFractalModel()
        {
            FractalBitmapModel = new FractalBitmapModel();
            FractalBytesModel = FractalBytesModel.GetInstance();
            SideLength = 1600;
            IterationsCount = 5;
        }
    }
}
