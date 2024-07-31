using System;
using SplashKitSDK;
using Idimon;

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
        private Dictionary<string, Items> _items = new Dictionary<string, Items>
        {
            { "Candy", new Items("Candy", "use to catch Idimon", "img\\items\\Candy2.png", 1, true, true, "Items") },
            { "Master Candy", new Items("Master Candy", "use to catch Idimon", "img\\items\\candy1.png", 1, true, true, "Items") }
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
            _gameMap = new Map(mapData, blockTypes, _window);

            _player = new Player("Ash", _characterImages, new Point2D() { X = 400, Y = 300 }, _window, _gameMap);
            _player.Inventory.AddItem(_items["Candy"]);
            _player.Inventory.AddItem(_items["Master Candy"]);

            _gameMenu = new GameMenu(_player, _window);

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
            _gameMenu.HandleInput();
            
        }
    }
}
