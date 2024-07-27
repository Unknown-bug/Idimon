using SplashKitSDK;

namespace Idimon
{
    public class InventoryMenu 
    {
        Window _window;
        int _width, _length;
        Point _point;

        public InventoryMenu(int width, int length, Point point, Window window)
        {
            _width = width;
            _length = length;
            _point = point;
            _window = window;
        }

        public void Open()
        {
            _window.Clear(Color.White);
            _window.FillRectangle(Color.Black, _point.X, _point.Y, _width, _length);
            _window.Refresh(60);
        }
    }
}