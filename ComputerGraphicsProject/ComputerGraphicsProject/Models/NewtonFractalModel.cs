using Microsoft.AspNetCore.Mvc;
using ComputerGraphicsProject.Controllers;
using ComputerGraphicsProject.ValidationAttributes;
namespace ComputerGraphicsProject.Models
{
    [NewtonCNotNull("Real c and imaginary c cant both be 0")]
    public class NewtonFractalModel
    {
        public FractalBitmapModel FractalBitmapModel { get; set; }
        public FractalBytesModel FractalBytesModel { get; set; }

        public int MaxIterations { get; set; }
        public double Threshold { get; set; }

        public int Exponent { get; set; }

        public int RealC { get; set; }
        
        public int ImaginaryC { get; set; }
        public NewtonFractalModel()
        {
            FractalBitmapModel = new FractalBitmapModel();
            FractalBytesModel = FractalBytesModel.GetInstance();
            MaxIterations = 100;
            Threshold = 0.0001;
            Exponent  = 4;
        }

    }
}
