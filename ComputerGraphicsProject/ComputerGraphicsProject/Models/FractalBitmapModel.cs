using System.Drawing;

namespace ComputerGraphicsProject.Models
{
    public class FractalBitmapModel
    {
        int width;
        int height;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width < 22500)
                {
                    width = value;
                }
                else
                {
                    throw new Exception("Width must be less than 22500");
                }

            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (width < 22500)
                {
                    height = value;
                }
                else
                {
                    throw new Exception("Height must be less than 22500");
                }

            }
        }

        Bitmap bitmap;
        public void CreateBitMap() {
            bitmap = new Bitmap(width, height);
        }
    }
}
