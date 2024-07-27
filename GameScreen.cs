using System;
using SplashKitSDK;

namespace Idimon
{
    public class GameScreen : Screens
    {
        private GameMenu _gameMenu;
        private Player _player;
        private Map _gameMap;
        private SplashKitSDK.Timer _timer;
        private char[,] mapData = {
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
        private Dictionary<char, Block> blockTypes = new Dictionary<char, Block>
        {
            { '*', new Block("grass", "img/Grass.png") },
            { 'x', new Block("stone", "img/PlayerIMG/down0.png") }
        };

        private List<string> _characterImages = new List<string>
        {
            "img\\PlayerIMG\\down0.png",
            "img\\PlayerIMG\\down1.png",
            "img\\PlayerIMG\\down2.png",
            "img\\PlayerIMG\\down3.png",
        };

        public GameScreen(Window window) : base(window)
        {
            _gameMenu = new GameMenu();
            _gameMap = new Map(mapData, blockTypes, _window);
            _player = new Player("Ash", _characterImages, new Point2D() { X = 400, Y = 300 }, _window, _gameMap);
            _timer = new SplashKitSDK.Timer("GameTimer");
            _timer.Start();
        }

        public override void Update()
        {
            double deltaTime = _timer.Ticks / 1000.0;
            _timer.Reset();

            if (!_gameMenu.Visible)
            {
                _player.Update(deltaTime);
            }
        }

        public override void Draw()
        {
            _window.Clear(Color.White);
            _gameMap.Draw(_player);
            _player.Draw();
            _gameMenu.Draw();
        }

        public override void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                _gameMenu.Toggle();
            }

            if (_gameMenu.Visible)
            {
                if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
                {
                    _gameMenu.Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
                }
                else if (SplashKit.KeyTyped(KeyCode.ZKey))
                {
                    _gameMenu.Select();
                }
            }
        }
    }
}
