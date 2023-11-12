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
            Pen pen = new Pen(System.Drawing.Color.Blue);

            Vicsek(bitmap, graphics, pen, width / 2, height / 2, length, iterationCount);
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
        private void DrawCross(Bitmap bitmap, Pen pen, int x, int y, int length)
        {
            int halfLength = length / 2;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawLine(pen, x - halfLength, y - length / 6, x + halfLength, y - length / 6);
                g.DrawLine(pen, x - halfLength, y + length / 6, x + halfLength, y + length / 6);
                g.DrawLine(pen, x, y - halfLength, x, y + halfLength);
            }
        }

        private void Vicsek(Bitmap bm, Graphics graphics, Pen pen, int x, int y, int length, int n)
        {
            if (n == 0)
            {
                DrawCross(bm, pen, x, y, length);
                return;
            }

            Vicsek(bm, graphics, pen, x, y, length / 3, n - 1);
            Vicsek(bm, graphics, pen, x + length / 3, y, length / 3, n - 1);
            Vicsek(bm, graphics, pen, x - length / 3, y, length / 3, n - 1);
            Vicsek(bm, graphics, pen, x, y + length / 3, length / 3, n - 1);
            Vicsek(bm, graphics, pen, x, y - length / 3, length / 3, n - 1);
        }
    }
}
