using SplashKitSDK;

namespace Idimon
{
    public class Game
    {
        private Window _window;
        // char[,] mapData = { 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}, 
        //     { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*'}
        //     };
        char[,] mapData = {
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', 'x', '*', '*', '*', '*', 'x', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', 'x', '*', '*', '*', '*', 'x', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', 'x', 'x', 'x', 'x', 'x', 'x', 'x', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
                { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' }
            };
        Dictionary<char, Block> blockTypes = new Dictionary<char, Block>
        {
            { '*', new Block("grass", "img/Grass.png") },
            // { 'x', new Block("stone", "img/stone.png") }
        };
        Map _gameMap;
        private Player _player;
        private SplashKitSDK.Timer _timer;
        private List<string> _characterImages = new List<string>
        {
            "img\\down0.png",
            "img\\down1.png",
            "img\\down2.png",
            "img\\down3.png",
            // "img\\up0.png",
            // "img\\up1.png",
            // "img\\up2.png",
            // "img\\up3.png",
            // "img\\left0.png",
            // "img\\left1.png",
            // "img\\left2.png",
            // "img\\left3.png",
            // "img\\right0.png",
            // "img\\right1.png",
            // "img\\right2.png",
            // "img\\right3.png"
        };

        public Game()
        {
            _window = new Window("Shape Drawer", 960, 768);
            _player = new Player("Ash", _characterImages, new Point2D() { X = 400, Y = 300 }, _window);
            _timer = new SplashKitSDK.Timer("GameTimer");
            _timer.Start();
            _gameMap = new Map(mapData, blockTypes, _window);
            
        }

        public void Run()
        {
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);
                _gameMap.Draw(_player);
                double deltaTime = _timer.Ticks / 1000.0;
                _timer.Reset();

                _player.HandleInput(deltaTime);

                if (_gameMap.CanMoveTo((int)(_player.Position.X + _player.MoveVector.X) / 32, (int)(_player.Position.Y + _player.MoveVector.Y) / 32))
                {
                    _player.Update(deltaTime);
                }

                _player.Update(deltaTime);
                _player.Draw();

                SplashKit.RefreshScreen();
            } while (!_window.CloseRequested);
        }
    }
}
