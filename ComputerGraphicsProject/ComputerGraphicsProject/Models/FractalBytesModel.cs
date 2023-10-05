namespace ComputerGraphicsProject.Models
{
    public class FractalBytesModel
    {
        public byte[] FractalBytes { get; set; }

        private static FractalBytesModel instance;

        // Приватний конструктор, щоб заборонити створення нових екземплярів класу
        private FractalBytesModel() { }

        // Публічний метод, який повертає єдиний екземпляр класу (лінива ініціалізація)
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
