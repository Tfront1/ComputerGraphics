namespace ComputerGraphicsProject.Models
{
    public class ColorBytesModel
    {
        public byte[] Bytes { get; set; }

        private static ColorBytesModel instance;

        private ColorBytesModel() { }

        public static ColorBytesModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ColorBytesModel();
            }
            return instance;
        }
    }
}
