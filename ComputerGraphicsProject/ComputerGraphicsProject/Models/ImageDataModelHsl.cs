namespace ComputerGraphicsProject.Models
{
    public class ImageDataModelHsl
    {
        public byte[] fullImageData { get; set; }
        public int startX { get; set; }
        public int startY { get; set; }
        public int endX { get; set; }
        public int endY { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public ImageDataModelHsl(byte[] fullImageData, int startX, int startY, int endX, int endY, int height, int width)
        {
            this.fullImageData = fullImageData;
            this.startX = startX;
            this.startY = startY;
            this.endX = endX;
            this.endY = endY;
            this.height = height;
            this.width = width;
        }
    }

}
