using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace ComputerGraphicsProject.Services
{
    public class NewtonFractalService : INewtonFractalService
    {
        public byte[] GenerateFractal(int width, int height, int maxIterations, double threshold, int exponent, Complex c)
        {
            Bitmap bitmap = new Bitmap(width, height);

            int numThreads = Environment.ProcessorCount;
            Task[] tasks = new Task[numThreads];

            for (int t = 0; t < numThreads; t++)
            {
                int threadId = t;

                tasks[t] = Task.Factory.StartNew(() =>
                {
                    for (int x = threadId; x < width; x += numThreads)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            double a = Map(x, 0, width, -2, 2);
                            double b = Map(y, 0, height, -2, 2);

                            Complex z = new Complex(a, b);

                            System.Drawing.Color pixelColor = GetColorForPixel(z, c, maxIterations, threshold, exponent);

                            lock (bitmap)
                            {
                                bitmap.SetPixel(x, y, pixelColor);
                            }
                        }
                    }
                });
            }
            Task.WaitAll(tasks);

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                byte[] result = ms.ToArray();
                return result;
            }
        }

        private System.Drawing.Color GetColorForPixel(Complex pixel, Complex c, int maxIterations, double threshold, int exponent)
        {
            //c = Complex.Pow(pixel, exponent) - 1;

            Complex[] roots = { 1, Complex.FromPolarCoordinates(1, 2 * Math.PI / 3), Complex.FromPolarCoordinates(1, -2 * Math.PI / 3) };

            int iterations = 0;
            Complex root = new Complex();

            while (iterations < maxIterations && Complex.Abs(Complex.Pow(pixel, exponent) + c) > threshold)
            {
                root = roots.OrderBy(r => Complex.Abs(pixel - r)).First();

                pixel -= (Complex.Pow(pixel, exponent) + c) / (exponent * Complex.Pow(pixel, exponent - 1));
                iterations++;
            }

            System.Drawing.Color color = new System.Drawing.Color();
            if (root == 1)
            {
                color = System.Drawing. Color.Red;
            }
            else if (root == Complex.FromPolarCoordinates(1, 2 * Math.PI / 3))
            {
                color = System.Drawing.Color.Green;
            }
            else
            {
                color = System.Drawing.Color.Blue;
            }

            return color;
        }

        private double Map(double num, double min1, double max1, double min2, double max2) => (num - min1) * (max2 - min2) / (max1 - min1) + min2;
    }
}
