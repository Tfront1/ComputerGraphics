using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsProject.Services
{
    public class CmykService : ICmykService
    {
        public Bitmap ConvertToBitmap(CmykModel model)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Копіювання потоку в пам'ять
                model.File.CopyTo(memoryStream);

                // Створення Bitmap з пам'яті
                var bitmap = new Bitmap(memoryStream);
                return bitmap;
            }
        }

        public byte[] GenerateCmyk(CmykModel model)
        {
            Bitmap bitmap = ConvertToBitmap(model);

            var file = model.File;
            byte[] fileBytes = new byte[] { };

            int width = bitmap.Width;
            int height = bitmap.Height;

            if (file != null && file.Length != 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Збереження зображення в потік пам'яті
                    bitmap.Save(memoryStream, ImageFormat.Bmp);

                    // Отримання масиву байтів
                    fileBytes = memoryStream.ToArray();
                }
            }

            return ConvertRgbToCmyk(fileBytes, width, height, model.C, model.M, model.Y);
        }

        private byte[] ConvertRgbToCmyk(byte[] rgbBytes, int width, int height, int C, int M, int Y)
        {
            byte[] result = new byte[rgbBytes.Length];
            int h = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(width, height))
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            byte red = (byte)(rgbBytes[h] - C);
                            byte green = (byte)(rgbBytes[h + 1] - M);
                            byte blue = (byte)(rgbBytes[h + 2] - Y);

                            double black = Math.Min(1.0 - red / 255.0, Math.Min(1.0 - green / 255.0, 1.0 - blue / 255.0));

                            double cyan, magenta, yellow;

                            if (black == 1)
                            {
                                cyan = 0.0;
                                magenta = 0.0;
                                yellow = 0.0;
                            }
                            else
                            {
                                cyan = (1.0 - (red / 255.0) - black) / (1.0 - black);
                                magenta = (1.0 - (green / 255.0) - black) / (1.0 - black);
                                yellow = (1.0 - (blue / 255.0) - black) / (1.0 - black);
                            }

                            var resultTemp = CmykToRgb(cyan, magenta, yellow, black);

                            result[h] = resultTemp[0];
                            result[h + 1] = resultTemp[1];
                            result[h + 2] = resultTemp[2];

                            // Зберегти отриманий бітмап в MemoryStream
                            bmp.SetPixel(width - x - 1, height - y - 1, Color.FromArgb(result[h], result[h + 1], result[h + 2]));
                            h += 3;
                        }
                    }

                    bmp.Save(ms, ImageFormat.Bmp);
                }

                // Повернути масив байтів з MemoryStream
                return ms.ToArray();
            }
        }

        static byte[] CmykToRgb(double cyan, double magenta, double yellow, double black)
        {
            byte red = Convert.ToByte((1 - Math.Min(1, cyan * (1 - black) + black)) * 255);
            byte green = Convert.ToByte((1 - Math.Min(1, magenta * (1 - black) + black)) * 255);
            byte blue = Convert.ToByte((1 - Math.Min(1, yellow * (1 - black) + black)) * 255);

            return new[] { red, green, blue };
        }
    }
}
