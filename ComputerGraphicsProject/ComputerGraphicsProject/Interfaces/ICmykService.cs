using ComputerGraphicsProject.Models;
using System.Drawing;

namespace ComputerGraphicsProject.Interfaces
{
    public interface ICmykService
    {
        public byte[] GenerateCmyk(CmykModel model);
        public byte[] GetPixelFromBitmapFromRgbToCmyk(CmykModel model, int x, int y);
    }
}
