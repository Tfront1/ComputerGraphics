namespace ComputerGraphicsProject.Models
{
    public class NewtonFractalModel
    {
        public FractalBitmapModel FractalBitmapModel { get; set; }
        public FractalBytesModel FractalBytesModel { get; set; }

        public int MaxIterations { get; set; }
        public double Threshold { get; set; }

        public int Exponent { get; set; }

        public int realC { get; set; }
        public int imaginaryC { get; set; }
        public NewtonFractalModel()
        {
            FractalBitmapModel = new FractalBitmapModel();
            MaxIterations = 100;
            Threshold = 0.0001;
            Exponent  = 4;
        }

    }
}
