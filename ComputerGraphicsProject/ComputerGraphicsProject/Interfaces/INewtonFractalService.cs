using System.Drawing;
using System.Numerics;

namespace ComputerGraphicsProject.Interfaces
{
    public interface INewtonFractalService
    {
        Bitmap GenerateFractal(int width, int height, int maxIterations, double threshold, int exponent, Complex c);
    }
}
