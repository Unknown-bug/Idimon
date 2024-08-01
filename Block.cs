using SplashKitSDK;

namespace Idimon
{
    public class Block
    {
        public string Type { get; set; }
        public Bitmap Image { get; set; }
        public bool IsSolid { get; set; }

        public Block(string type, string imagePath, bool isSolid)
        {
            Type = type;
            Image = SplashKit.LoadBitmap(type, imagePath);
            IsSolid = isSolid;
        }

        public void Draw(Window window, double x, double y)
        {
            window.DrawBitmap(Image, x, y);
        }
    }
}
