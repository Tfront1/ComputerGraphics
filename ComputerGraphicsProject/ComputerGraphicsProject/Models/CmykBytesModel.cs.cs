namespace ComputerGraphicsProject.Models
{
    public class CmykBytesModel
    {
        public byte[] Bytes { get; set; }

        private static CmykBytesModel instance;

        private CmykBytesModel() { }

        public static CmykBytesModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CmykBytesModel();
            }
            return instance;
        }
    }
}
