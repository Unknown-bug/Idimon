using SplashKitSDK;

namespace Idimon
{
    public class Game
    {
        private Window _window;
        public static Screens CurrentScreen { get; set; }

        public Game()
        {
            _window = new Window("Shape Drawer", 960, 768);
            CurrentScreen = new TitleScreen(_window);
        }

        public void Run()
        {
            do
            {
                SplashKit.ProcessEvents();
                CurrentScreen.HandleInput();
                CurrentScreen.Update();
                CurrentScreen.Draw();
                SplashKit.RefreshScreen();
            } while (!_window.CloseRequested);
        }
    }
}
