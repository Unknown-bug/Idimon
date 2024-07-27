using SplashKitSDK;

namespace Idimon
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }

        public Item(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            Image = SplashKit.LoadBitmap(name, imagePath);
        }

        public void Draw(Window window, double x, double y)
        {
            window.DrawBitmap(Image, x, y/*, SplashKit.OptionScaleBmp(2,2)*/);
        }
    }
}
