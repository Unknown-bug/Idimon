using SplashKitSDK;

namespace Idimon
{
    public class NPC : Character
    {
        string path = "img\\NPCIMG\\";
        public NPC(string name, List<string> imagePaths, Point2D position, Window window) : base(name, imagePaths, position, window)
        {
            LoadImages();
            // _moveQueue = new Queue<Point2D>();
            // _moveTimer = 0;
            _window = window;
        }

        public override void LoadImages()
        {
            _images["up"] = new Bitmap[4];
            _images["down"] = new Bitmap[4];
            _images["left"] = new Bitmap[4];
            _images["right"] = new Bitmap[4];

            for (int i = 0; i < 1; i++)
            {
                _images["up"][i] = SplashKit.LoadBitmap($"up_{i}", $"{path}up{i}.png");
                _images["down"][i] = SplashKit.LoadBitmap($"down_{i}", $"{path}down{i}.png");
                _images["left"][i] = SplashKit.LoadBitmap($"left_{i}", $"{path}left{i}.png");
                _images["right"][i] = SplashKit.LoadBitmap($"right_{i}", $"{path}right{i}.png");
            }
        }
        
    }
}