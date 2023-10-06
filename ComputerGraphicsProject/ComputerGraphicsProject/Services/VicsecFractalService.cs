using System.Drawing;
using System.Drawing.Imaging;
using ComputerGraphicsProject.Interfaces;
using System.Numerics;

namespace ComputerGraphicsProject.Services
{
    public class VicsecFractalService : IVicsecFractalService
    {
        public byte[] GenerateFractal(int width, int height, int length, int iterationCount) {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Blue);

            Vicsek(graphics, pen, width / 2, height / 2, length, iterationCount);
            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                result = ms.ToArray();
            }
            graphics.Dispose();
            bitmap.Dispose();
            return result;
        }
        public void DrawCross(Graphics graphics, Pen pen, int x, int y, int length)
        {
            int halfLength = length / 2;

            Point[] points = new Point[12];

            points[0] = new Point(x - halfLength, y - length / 6);
            points[1] = new Point(x - halfLength + length / 3, y - length / 6);
            points[2] = new Point(x - halfLength + length / 3, y + length / 6);
            points[3] = new Point(x - halfLength + length, y + length / 6);
            points[4] = new Point(x - halfLength + length, y + halfLength);
            points[5] = new Point(x + halfLength, y + halfLength);
            points[6] = new Point(x + halfLength, y + length / 6);
            points[7] = new Point(x + halfLength - length / 3, y + length / 6);
            points[8] = new Point(x + halfLength - length / 3, y - length / 6);
            points[9] = new Point(x + halfLength - length, y - length / 6);
            points[10] = new Point(x + halfLength - length, y - halfLength);
            points[11] = new Point(x - halfLength, y - halfLength);

            graphics.DrawPolygon(pen, points);
        }
        public void Vicsek(Graphics graphics, Pen pen, int x, int y, int length, int n)
        {
            if (n == 0)
            {
                DrawCross(graphics, pen, x, y, length);
                return;
            }

            Vicsek(graphics, pen, x, y, length / 3, n - 1);
            Vicsek(graphics, pen, x + length / 3, y, length / 3, n - 1);
            Vicsek(graphics, pen, x - length / 3, y, length / 3, n - 1);
            Vicsek(graphics, pen, x, y + length / 3, length / 3, n - 1);
            Vicsek(graphics, pen, x, y - length / 3, length / 3, n - 1);
        }
    }
}
