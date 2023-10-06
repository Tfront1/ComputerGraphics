namespace ComputerGraphicsProject.Interfaces
{
    public interface IVicsecFractalService
    {
        public byte[] GenerateFractal(int width, int height, int length, int iterationCount);
    }
}
