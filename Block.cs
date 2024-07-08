using SplashKitSDK;

namespace Idimon
{
    public class Block
    {
        public string Type { get; set; }
        public Bitmap Image { get; set; }

        public Block(string type, string imagePath)
        {
            Type = type;
            Image = SplashKit.LoadBitmap(type, imagePath);
        }

        public void Draw(Window window, double x, double y)
        {
            window.DrawBitmap(Image, x, y);
        }
    }
}
