namespace ComputerGraphicsProject.Models
{
    public class VicsekFractalModel
    {
        public FractalBitmapModel FractalBitmapModel { get; set; }
        public FractalBytesModel FractalBytesModel { get; set; }
        
        public int SideLength { get; set; }
        public int IterationsCount { get; set; }

        public VicsekFractalModel()
        {
            FractalBitmapModel = new FractalBitmapModel();
            SideLength = 1600;
            IterationsCount = 5;
        }
    }
}
