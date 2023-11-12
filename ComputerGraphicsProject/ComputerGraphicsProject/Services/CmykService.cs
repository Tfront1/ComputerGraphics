using ComputerGraphicsProject.Interfaces;
using ComputerGraphicsProject.Models;
using System.Drawing.Imaging;
using System.Drawing;

namespace ComputerGraphicsProject.Services
{
    public class CmykService : ICmykService
    {
        public byte[] GenerateCmyk(CmykModel model)
        {
            var file = model.File;
            byte[] fileBytes = new byte[] { };
            if (file != null && file.Length != 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }
            }
            return ConvertRgbToCmyk(fileBytes, model.C, model.M, model.Y);
        }

        private byte[] ConvertRgbToCmyk(byte[] rgbBytes,int C,int M,int Y)
        {
            int pixelCount = rgbBytes.Length / 3;
            double[] resBytes = new double[rgbBytes.Length];
            byte[] result = new byte[rgbBytes.Length];
            for (int i = 0; i < pixelCount; i++)
            {
                int startIndex = i * 3;
                byte red = (byte)(rgbBytes[startIndex] - C);
                byte green = (byte)(rgbBytes[startIndex + 1] - M);
                byte blue = (byte)(rgbBytes[startIndex + 2] - Y);

                double black = Math.Min(1.0 - red / 255.0, Math.Min(1.0 - green / 255.0, 1.0 - blue / 255.0));
                double cyan = (1.0 - (red / 255.0) - black) / (1.0 - black);
                double magenta = (1.0 - (green / 255.0) - black) / (1.0 - black);
                double yellow = (1.0 - (blue / 255.0) - black) / (1.0 - black);
                if (black == 1) {
                    cyan = 0.0;
                    magenta = 0.0;
                    yellow = 0.0;
                }
                //if (C != 0) {
                //    cyan = C / 255.0;
                //}
                //if (M != 0) {
                //    magenta= M / 255.0;
                //}
                //if (Y != 0) {
                //    yellow= Y / 255.0;
                //}

                resBytes[startIndex] = cyan;
                resBytes[startIndex + 1] = magenta;
                resBytes[startIndex + 2] = yellow;

                var resultTemp = CmykToRgb(cyan, magenta, yellow, black);

                result[startIndex] = resultTemp[0];
                result[startIndex+1] = resultTemp[1];
                result[startIndex+2] = resultTemp[2];
                
            }
            
            return result;
        }
        private byte NormalizeColor(double color)
        {
            return (byte)(color * 255);
        }

        static byte[] CmykToRgb(double cyan, double magenta, double yellow, double black)
        {
            byte red = Convert.ToByte((1 - Math.Min(1, cyan * (1 - black) + black)) * 255);
            byte green = Convert.ToByte((1 - Math.Min(1, magenta * (1 - black) + black)) * 255);
            byte blue = Convert.ToByte((1 - Math.Min(1, yellow * (1 - black) + black)) * 255);

            return new[] { red, green, blue };
        }
        //private byte[] CmykToRgb(double[] bytes) {

        //    int width = 100;
        //    int height = 100;
        //    Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        //    for (int y = 0; y < height; y++)
        //    {
        //        for (int x = 0; x < width; x++)
        //        {
        //            int index = (y * width + x) * 4;

        //            int red = (int)((1 - Math.Min(1, bytes[index] * (1 - bytes[index + 3]) + bytes[index + 3])) * 255);
        //            int green = (int)((1 - Math.Min(1, bytes[index + 1] * (1 - bytes[index + 3]) + bytes[index + 3])) * 255);
        //            int blue = (int)((1 - Math.Min(1, bytes[index + 2] * (1 - bytes[index + 3]) + bytes[index + 3])) * 255);

        //            bmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
        //        }
        //    }
        //    byte[] result = new byte[] { };
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        bmp.Save(stream, ImageFormat.Jpeg);
        //        result = stream.ToArray();
        //    }
        //    return result;
        //}
    }
}
