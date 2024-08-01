using System;
using SplashKitSDK;

namespace Idimon
{
    public class BattleScreen : Screens
    {
        List<Idimons> _player;
        List<Idimons> _opponent;
        Inventory _inventory;
        Window _window;
        GameScreen _preGameScreen;
        private int _currentIdimonIndex;
        Bitmap _background;
        List<MenuItem> _actionButtons;
        private enum Action
        {
            Skills,
            Swich,
            Items,
            Run
        }

        public BattleScreen(Window window, List<Idimons> player, List<Idimons> opponents, Inventory inventory, GameScreen preGameScreen) : base(window)
        {
            _window = window;
            _player = player;
            _opponent = opponents;
            _inventory = inventory;
            _preGameScreen = preGameScreen;
            _background = new Bitmap("battle", "img\\CombatBackground.jpg");
            _currentIdimonIndex = 0;
            _actionButtons = new List<MenuItem>
            {
                new MenuItem("Skills", 50, SplashKit.ScreenHeight() / 3 * 2 + 30),
                new MenuItem("Switch", 50, SplashKit.ScreenHeight() / 3 * 2 + 80),
                new MenuItem("Items", 50, SplashKit.ScreenHeight() / 3 * 2 + 130),
                new MenuItem("Run", 50, SplashKit.ScreenHeight() / 3 * 2 + 180)
            };
        }

        public override void Update()
        {
            // throw new NotImplementedException();

        }
        
        public override void Draw()
        {
            // Initialize the window
            _window.Clear(Color.Black);
            SplashKit.FillRectangle(Color.White, 0, SplashKit.ScreenHeight() / 3 * 2, SplashKit.ScreenWidth(), 2);
            SplashKit.FillRectangle(Color.White, SplashKit.ScreenWidth() / 4, SplashKit.ScreenHeight() / 3 * 2, 2, SplashKit.ScreenHeight());
            foreach (MenuItem button in _actionButtons)
            {
                button.Draw();
            }

            // Player
            _player[_currentIdimonIndex].Draw(SplashKit.ScreenWidth() / 2 - 300, 200);
            _player[_currentIdimonIndex].DrawHPBar(20, 20, SplashKit.ScreenWidth() / 3, 20);

            // Opponent
            Idimons opponent = _opponent[0];
            opponent.Draw(SplashKit.ScreenWidth() / 2 + 300, 200);
            opponent.DrawHPBar(SplashKit.ScreenWidth() - (SplashKit.ScreenWidth() / 3 + 20) , 20, SplashKit.ScreenWidth() / 3, 20);


            _window.Refresh(60);
        }

        public override void HandleInput()
        {
            if(SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                // Implement battle logic
            }
            else if(SplashKit.KeyTyped(KeyCode.XKey))
            {
                Game.CurrentScreen = _preGameScreen;
            }
            // throw new NotImplementedException();
        }
    }
}