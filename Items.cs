using SplashKitSDK;

namespace Idimon
{
    public class Items
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }
        public Bitmap Background { get; set; }
        public bool IsSelected { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Quantity { get; set; }
        public bool IsEquippable { get; set; }
        public bool IsConsumable { get; set; }
        public string Type { get; set; }

        // public Items(string name, string description, string imagePath, double x, double y, int quantity, bool isEquippable, bool isConsumable)
        // {
        //     Name = name;
        //     Description = description;
        //     Image = SplashKit.LoadBitmap(name, imagePath);
        //     X = x;
        //     Y = y;
        //     Quantity = quantity;
        //     IsEquippable = isEquippable;
        //     IsConsumable = isConsumable;
        // }
        public Items(string name, string description, string imagePath, int quantity, bool isEquippable, bool isConsumable, string type)
        {
            Name = name;
            Description = description;
            Image = SplashKit.LoadBitmap(name, imagePath);
            Background = SplashKit.LoadBitmap("Background", "img\\Items\\background.png");
            Quantity = quantity;
            IsEquippable = isEquippable;
            IsConsumable = isConsumable;
            Type = type;
        }

        public void Draw(Window window, double x, double y)
        {
            SplashKit.LoadFont("Arial", "arial.ttf");
           
            SplashKit.DrawRectangle(Color.RGBColor(255, 255, 255), x - 5, y - 5, SplashKit.ScreenWidth() / 3, 40);
            window.DrawBitmap(Background, x, y/*, SplashKit.OptionScaleBmp(2,2)*/);
            window.DrawBitmap(Image, x, y/*, SplashKit.OptionScaleBmp(2,2)*/);

            SplashKit.DrawText(Quantity.ToString(), Color.White, "Arial", 30, x + SplashKit.ScreenWidth() / 3 - 50, y);

            if (IsSelected)
            {
                SplashKit.FillRectangle(Color.Black, x - 5, y - 5, SplashKit.TextWidth(Name, "Arial", 30) + 10, SplashKit.TextHeight(Name, "Arial", 30) + 10);
                SplashKit.DrawText(": " + Name, Color.Yellow, "Arial", 30, x + 35, y );
            }
            else
            {
                SplashKit.DrawText(Name, Color.White, "Arial", 30, x + 35, y );
            }
        }
    }
}
