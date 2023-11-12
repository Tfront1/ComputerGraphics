using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsProject.Services
{
    public class HslService : IHslService
    {
        public byte[] GenerateHsl(HslModel model)
        {
            Bitmap bitmap = GenerateBitmap(100, 100, model.H, model.S, model.L);

            // Convert the bitmap to a byte array
            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                result = stream.ToArray();
            }

            return result;
        }
        static Bitmap GenerateBitmap(int width, int height, double h, double s, double l)
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
        }

        static Color HSLtoRGB(double h, double s, double l)
        {
            double r, g, b;

            if (s == 0)
            {
                r = g = b = l; // achromatic
            }
            else
            {
                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                r = HueToRGB(p, q, h + 1.0 / 3);
                g = HueToRGB(p, q, h);
                b = HueToRGB(p, q, h - 1.0 / 3);
            }

            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        static double HueToRGB(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1.0 / 6) return p + (q - p) * 6 * t;
            if (t < 1.0 / 2) return q;
            if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
            return p;
        }
    }
}

