using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsProject.Services
{
    public class CmykService : ICmykService
    {
        private Bitmap ConvertToBitmap(CmykModel model)
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

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);

                return ms.ToArray();
            }
            /*
            int width = bitmap.Width;
            int height = bitmap.Height;

            byte[] pixelBytes = GetPixelBytes(bitmap, width, height);

            return pixelBytes;
            */
            //return ConvertRgbToCmyk(pixelBytes, width, height, model.C, model.M, model.Y, bitmap);
        }

        private byte[] GetPixelBytes(Bitmap bitmap, int width, int height)
        {
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int stride = ((width * bytesPerPixel + 3) / 4) * 4;  // Забезпечити вирівнювання до кратного 4
            int pixelArraySize = stride * height;

            byte[] rgbValues = new byte[pixelArraySize];
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rgbValues, 0, pixelArraySize);

            bitmap.UnlockBits(bmpData);

            return rgbValues;
        }

        private byte[] ConvertRgbToCmyk(byte[] rgbBytes, int width, int height, int C, int M, int Y, Bitmap bitmap)
        {
            byte[] result = new byte[rgbBytes.Length];

            int h = 0;

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int stride = ((width * bytesPerPixel + 3) / 4) * 4;

            int skipBytes = stride - width * bytesPerPixel; // Кількість байтів, які потрібно пропустити на кожному рядку


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
                            bmp.SetPixel(x, y, Color.FromArgb(result[h], result[h + 1], result[h + 2]));
                            h += 3;
                        }
                        h += skipBytes; // Пропустити невикористані байти на кожному рядку
                    }

                    bool fl = false;

                    if(rgbBytes.Equals(result))
                    {
                        fl = true;
                    }

                    bmp.Save(ms, ImageFormat.Jpeg);
                }

                // Повернути масив байтів з MemoryStream
                return ms.ToArray();
            }
        }

        private byte[] CmykToRgb(double cyan, double magenta, double yellow, double black)
        {
            byte red = Convert.ToByte((1 - Math.Min(1, cyan * (1 - black) + black)) * 255);
            byte green = Convert.ToByte((1 - Math.Min(1, magenta * (1 - black) + black)) * 255);
            byte blue = Convert.ToByte((1 - Math.Min(1, yellow * (1 - black) + black)) * 255);

            return new[] { red, green, blue };
        }
        
        public byte[] GetPixelFromBitmapFromRgbToCmyk(CmykModel model, int x,  int y) 
        {
            Bitmap bitmap = ConvertToBitmap(model);

            Color pixelColor = bitmap.GetPixel(x, y);

            byte[] cmykBytes = RgbToCmykBytes(pixelColor.R, pixelColor.G, pixelColor.B);

            return cmykBytes;
        }

        public static byte[] RgbToCmykBytes(int red, int green, int blue)
        {
            // Нормалізація значень RGB до діапазону [0, 1]
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            // Обчислення значень CMY
            double c = 1.0 - r;
            double m = 1.0 - g;
            double y = 1.0 - b;

            // Визначення значення K (чорнила)
            double k = Math.Min(c, Math.Min(m, y));

            // Обчислення значень CMYK
            double cyan = (c - k) / (1.0 - k);
            double magenta = (m - k) / (1.0 - k);
            double yellow = (y - k) / (1.0 - k);
            double black = k;

            // Масштабування значень CMYK до діапазону [0, 255] і перетворення їх в байти
            byte[] cmykBytes = new byte[]
            {
            (byte)(cyan * 255),
            (byte)(magenta * 255),
            (byte)(yellow * 255),
            (byte)(black * 255)
            };

            return cmykBytes;
        }
    }
}
