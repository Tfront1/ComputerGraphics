namespace ComputerGraphicsProject.Models
{ 
    public class FractalBytesModel
    {
        public byte[] FractalBytes { get; set; }
        public int? LastGeneratedFractal { get; set; }

        private static FractalBytesModel instance;

        private FractalBytesModel() { }

        public static FractalBytesModel GetInstance()
        {
            if (instance == null)
            {
                instance = new FractalBytesModel();
            }
            return instance;
        }

    }
}
