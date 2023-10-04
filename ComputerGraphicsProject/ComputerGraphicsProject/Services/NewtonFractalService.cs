using ComputerGraphicsProject.Interfaces;
using System.Drawing;
using System.Numerics;

namespace ComputerGraphicsProject.Services
{
    public class NewtonFractalService : INewtonFractalService
    {
        public Bitmap GenerateFractal(int width, int height, int maxIterations, double threshold, int exponent,Complex c)
        {
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double a = Map(x, 0, width, -2, 2);
                    double b = Map(y, 0, height, -2, 2);

                    Complex z = new Complex(a, b);

                    // Modify this part to generate images for different values of 'c'
                    c = new Complex(1, 1); // Change 'c' as needed   
                    Color pixelColor = GetColorForPixel(z, c, maxIterations, threshold, exponent);

                    bitmap.SetPixel(x, y, pixelColor);
                }
            }
            return bitmap;
        }

        private Color GetColorForPixel(Complex pixel, Complex c, int maxIterations, double threshold, int exponent)
        {
            c = Complex.Pow(pixel, exponent) - 1;
            //c = Complex.Pow(0, 0);
            Complex[] roots = { 1, Complex.FromPolarCoordinates(1, 2 * Math.PI / 3), Complex.FromPolarCoordinates(1, -2 * Math.PI / 3) };

            // Initialize variables for tracking the root and iteration count

            int iterations = 0;
            Complex root = new Complex();
            // Iterate the function until it converges or the maximum number of iterations is reached
            while (iterations < maxIterations && Complex.Abs(Complex.Pow(pixel, exponent) + c) > threshold)
            {
                root = roots.OrderBy(r => Complex.Abs(pixel - r)).First();
                // Calculate the next iteration using the Newton-Raphson method
                pixel -= (Complex.Pow(pixel, exponent) + c) / (exponent * Complex.Pow(pixel, exponent - 1));
                iterations++;
            }

            // Interpolate the color of the pixel based on the root it converges to
            // Modify this part to assign colors based on the roots or other criteria
            // For example, you can use a gradient of colors based on the number of iterations.
            Color color = new Color();
            if (root == 1)
            {
                color = Color.Red; // root 1
            }
            else if (root == Complex.FromPolarCoordinates(1, 2 * Math.PI / 3))
            {
                color = Color.Green; // root 2
            }
            else
            {
                color = Color.Blue; // root 3
            }

            return color;
        }

        private double Map(double num, double min1, double max1, double min2, double max2)
        {
            return (num - min1) * (max2 - min2) / (max1 - min1) + min2;
        }
    }
}
