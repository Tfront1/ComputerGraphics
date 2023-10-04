namespace ComputerGraphicsProject.Models
{
    public class NewtonFractalModel
    {
        FractalBitmapModel fractalBitmapModel;

        public int MaxIterations { get; set; }
        public double Threshold { get; set; }

        int exponent = 4;
        public NewtonFractalModel()
        {
            fractalBitmapModel = new FractalBitmapModel();
            MaxIterations = 100;
            Threshold = 0.0001;
        }

    }
}
