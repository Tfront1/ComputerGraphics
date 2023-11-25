using Aspose.Svg.Drawing;
using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsProject.Services
{
    public class HslService : IHslService
    {
        private Bitmap ConvertToBitmap(HslModel model)
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

        public byte[] GenerateHsl(HslModel model)
        {
            Bitmap bitmap = ConvertToBitmap(model);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);

                return ms.ToArray();
            }
        }
/*        static Bitmap GenerateBitmap(int width, int height, double h, double s, double l)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Convert HSL to RGB using the adjusted ranges
                    Color color = HSLtoRGB(h / 360.0, s / 100.0, l / 100.0);

                    // Set the pixel color in the bitmap
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }*/

        public static void HslToRgb(double h, double s, double l, out int red, out int green, out int blue)
        {
            // Нормалізація відтінку, насиченості і яскравості до відповідних діапазонів
            h /= 360;
            s /= 100;
            l /= 100;

            double r, g, b;

            if (s == 0)
            {
                // Відтінок не визначений для відтінків від сірого
                r = g = b = l;
            }
            else
            {
                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;

                r = HueToRgb(p, q, h + 1 / 3.0);
                g = HueToRgb(p, q, h);
                b = HueToRgb(p, q, h - 1 / 3.0);
            }

            // Перетворення від [0, 1] до [0, 255]
            red = (int)(r * 255);
            green = (int)(g * 255);
            blue = (int)(b * 255);
        }

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1 / 6.0) return p + (q - p) * 6 * t;
            if (t < 1 / 2.0) return q;
            if (t < 2 / 3.0) return p + (q - p) * (2 / 3.0 - t) * 6;
            return p;
        }

        public static byte[] RemakeSelectedAreaByHsl(ImageDataModelHsl imageDataModelHsl, int h, int s, int l)
        {
            byte[] result = new byte[imageDataModelHsl.fullImageData.Length];
            int height = imageDataModelHsl.height;
            int width = imageDataModelHsl.width;

            int bytesPerPixel = 32 / 8;
            int stride = ((width * bytesPerPixel + 3) / 4) * 4;

            int skipBytes = stride - width * bytesPerPixel;

            int iterator = 0;

            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(width, height))
                {
                    for (int x = 0; x < height; x++)
                    {
                        for (int y = 0; y < width; y++)
                        {
                            if (IsPixelInSelectedArea(x, y, imageDataModelHsl.startX, imageDataModelHsl.startY, imageDataModelHsl.endX, imageDataModelHsl.endY))
                            {
                                int red = imageDataModelHsl.fullImageData[iterator];
                                int green = imageDataModelHsl.fullImageData[iterator + 1];
                                int blue = imageDataModelHsl.fullImageData[iterator + 2];

                                double hh, ss, ll;
                                RgbToHsl(red, green, blue, out hh, out ss, out ll);
                                ss = ss > s ? ss - s : s - ss;
                                ll = ll > l ? ll - l : l - ll;

                                HslToRgb(hh, ss, ll, out red, out green, out blue);
                                result[iterator] = (byte)red;
                                result[iterator + 1] = (byte)green;
                                result[iterator + 2] = (byte)blue;
                                result[iterator + 3] = imageDataModelHsl.fullImageData[iterator + 3];
                            }
                            else
                            {
                                result[iterator] = imageDataModelHsl.fullImageData[iterator];
                                result[iterator + 1] = imageDataModelHsl.fullImageData[iterator + 1];
                                result[iterator + 2] = imageDataModelHsl.fullImageData[iterator + 2];
                                result[iterator + 3] = imageDataModelHsl.fullImageData[iterator + 3];
                            }
                            iterator += 4;
                        }
                        iterator += skipBytes;
                    }
                }
            }

            



            return result;
        }

        private static bool IsPixelInSelectedArea(int x, int y, int startX, int startY, int endX, int endY)
        {
            int minX = Math.Min(startX, endX);
            int maxX = Math.Max(startX, endX);

            int minY = Math.Min(startY, endY);
            int maxY = Math.Max(startY, endY);

            // Перевірка, чи координати пікселя знаходяться в зоні для зміни
            return x >= minX && x <= maxX && y >= minY && y <= maxY;
        }


        public static void RgbToHsl(int red, int green, int blue, out double h, out double s, out double l)
        {
            double r = red / 255.0;
            double g = green / 255.0;
            double b = blue / 255.0;

            double max = Math.Max(Math.Max(r, g), b);
            double min = Math.Min(Math.Min(r, g), b);

            // Обчислення яскравості (lightness)
            l = (max + min) / 2.0;

            // Обчислення насиченості (saturation)
            if (max == min)
            {
                s = 0;
            }
            else
            {
                s = l < 0.5 ? (max - min) / (max + min) : (max - min) / (2.0 - max - min);
            }

            // Обчислення відтінку (hue)
            if (max == min)
            {
                h = 0;
            }
            else if (max == r)
            {
                h = (g - b) / (max - min) + (g < b ? 6 : 0);
            }
            else if (max == g)
            {
                h = (b - r) / (max - min) + 2;
            }
            else
            {
                h = (r - g) / (max - min) + 4;
            }

            // Нормалізація відтінку до діапазону [0, 360)
            h *= 60;

            // Нормалізація насиченості і яскравості до діапазону [0, 1]
            s *= 100;
            l *= 100;
        }
    }
}

