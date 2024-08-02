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
        private int _currentIdimonIndex, _selectedIndex;
        Bitmap _background;
        List<MenuItem> _actionButtons, _skillsButtons;
        private enum Action
        {
            Skills,
            Swich,
            Items,
            Run
        }
        private enum Turn
        {
            Player,
            Opponent
        }
        private enum SkillsButton
        {
            Skill1,
            Skill2,
            Skill3,
            Skill4
        }

        private Turn _currentTurn;
        private Action _currentAction;
        private Battle _battle;
        private string _type;

        private int _currentSkillIndex;
        private int _selectedSkillIndex;

        public BattleScreen(Window window, List<Idimons> player, List<Idimons> opponents, Inventory inventory, GameScreen preGameScreen) : base(window)
        {
            _window = window;
            _player = player;
            _opponent = opponents;
            _inventory = inventory;
            _preGameScreen = preGameScreen;
            _background = new Bitmap("battle", "img\\CombatBackground.jpg");
            _currentIdimonIndex = 0;
            _selectedIndex = 0;
            _currentAction = (Action)0;
            _type = "";
            _actionButtons = new List<MenuItem>
            {
                new MenuItem("Skills", 50, SplashKit.ScreenHeight() / 3 * 2 + 30),
                new MenuItem("Switch", 50, SplashKit.ScreenHeight() / 3 * 2 + 80),
                new MenuItem("Items", 50, SplashKit.ScreenHeight() / 3 * 2 + 130),
                new MenuItem("Run", 50, SplashKit.ScreenHeight() / 3 * 2 + 180)
            };
            _actionButtons[_selectedIndex].IsSelected = true;
            _battle = new Battle(player, opponents, this);
            _currentTurn = DetermineFirstTurn();

            _selectedSkillIndex = 0;
        }

        public void ExitBattle()
        {
            Game.CurrentScreen = _preGameScreen;
        }

        public void Select()
        {
            switch (_currentAction)
            {
                case Action.Skills:
                    // Skills logic
                    _type = "Skills";
                    break;
                case Action.Swich:
                    // Items logic
                    _type = "Switch";
                    break;
                case Action.Items:
                    // Equipment logic
                    _type = "Items";
                    break;
                case Action.Run:
                    // Run logic
                    ExitBattle();
                    break;
            }
        }

        public override void Update()
        {
            // throw new NotImplementedException();
            // Console.WriteLine(_player[0].Skills[0].Damage);
            // ExecuteTurn();
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

            if(_type == "Skills")
            {
                // Draw skills
                double x = 300;
                double y = 555;
                int i = 0;

                foreach (var skill in _player[_currentIdimonIndex].Skills)
                {
                    if (i == _selectedSkillIndex)
                    {
                        SplashKit.FillRectangle(Color.RGBAColor(255, 255, 0, 150), x - 5, y - 5, 250, 30);
                    }

                    SplashKit.DrawText(skill.Name, Color.White, "Arial", 20, x, y);
                    SplashKit.DrawText($"Power: {skill.Damage}", Color.White, "Arial", 20, x + 150, y);

                    i++;
                    // y += SplashKit.ScreenHeight() / 6;
                    if(i % 2 == 0)
                    {
                        y += 50;
                        x = 300;
                    }
                    else
                    {
                        x += 300;
                    }
                }
            }

            _window.Refresh(60);
        }

        public void Navigate(KeyCode key)
        {
            if (_type == "Skills")
            {
                if (key == KeyCode.DownKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex + 2) % _actionButtons.Count;
                }
                else if (key == KeyCode.UpKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex - 2 + _actionButtons.Count) % _actionButtons.Count;
                }
                else if (key == KeyCode.RightKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex + 1) % _actionButtons.Count;
                }
                else if (key == KeyCode.LeftKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex - 1 + _actionButtons.Count) % _actionButtons.Count;
                }
                return;
            }
            _actionButtons[_selectedIndex].IsSelected = false;

            if (key == KeyCode.DownKey)
            {
                _selectedIndex = (_selectedIndex + 1) % _actionButtons.Count;
            }
            else if (key == KeyCode.UpKey)
            {
                _selectedIndex = (_selectedIndex - 1 + _actionButtons.Count) % _actionButtons.Count;
            }

            _actionButtons[_selectedIndex].IsSelected = true;
            _currentAction = (Action)_selectedIndex;

            
        }

        public override void HandleInput()
        {
            if(_type == "Skills")
            {
                
                HandleSkillsInput();
                return;
            }

            if(SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                // Implement battle logic
                Select();
                SplashKit.Delay(100);
            }
            
            else if(SplashKit.KeyTyped(KeyCode.XKey))
            {
                ExitBattle();
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            // throw new NotImplementedException();
        }



        public void HandleSkillsInput()
        {
            if(SplashKit.KeyTyped(KeyCode.XKey))
            {
                _type = "";
                SplashKit.Delay(100);
            }
            else if(SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                Console.WriteLine(_selectedSkillIndex);
                ExecuteTurn(_selectedSkillIndex);
                _currentTurn = Turn.Opponent;
            }
            else if(SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            else if(SplashKit.KeyTyped(KeyCode.RightKey) || SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.RightKey) ? KeyCode.RightKey : KeyCode.LeftKey);
            }
        }

        private void ExecuteTurn(int selectedSkillIndex)
        {
            Idimons playerIdimon = _player[_currentIdimonIndex];
            Idimons opponentIdimon = _opponent[0];

            if (_currentTurn == Turn.Player)
            {
                ExecutePlayerTurn(playerIdimon, opponentIdimon, selectedSkillIndex);
                ExecuteOpponentTurn(playerIdimon, opponentIdimon);
            }
            else
            {
                ExecuteOpponentTurn(playerIdimon, opponentIdimon);
                ExecutePlayerTurn(playerIdimon, opponentIdimon, selectedSkillIndex);
            }

            // After both turns, check for fainted Idimons and switch turns
            if (!playerIdimon.IsFainted() && !opponentIdimon.IsFainted())
            {
                _currentTurn = DetermineFirstTurn();
            }
            if (playerIdimon.IsFainted())
            {
                playerIdimon.Faint();
                SwitchPlayerIdimon(_currentIdimonIndex + 1);
                // Logic to switch Idimons
            }
            else if (opponentIdimon.IsFainted())
            {
                opponentIdimon.Faint();
                ExitBattle();
                // Logic for winning the battle
            }

            // Check if battle ends or continues
        }

        private void ExecutePlayerTurn(Idimons playerIdimon, Idimons opponentIdimon, int selectedSkillIndex)
        {
            // Handle player's move, currently using a placeholder skill index
            playerIdimon.Attacking(opponentIdimon, selectedSkillIndex);
            _currentTurn = Turn.Opponent;
        }

        private void ExecuteOpponentTurn(Idimons playerIdimon, Idimons opponentIdimon)
        {
            // Simple AI: randomly select a skill
            Random rand = new Random();
            int selectedSkillIndex = rand.Next(0, opponentIdimon.Skills.Count);
            opponentIdimon.Attacking(playerIdimon, selectedSkillIndex);
            _currentTurn = Turn.Player;
        }

        private Turn DetermineFirstTurn()
        {
            Idimons playerIdimon = _player[_currentIdimonIndex];
            Idimons opponentIdimon = _opponent[0];

            if (playerIdimon.Speed > opponentIdimon.Speed)
            {
                return Turn.Player;
            }
            else if (playerIdimon.Speed < opponentIdimon.Speed)
            {
                return Turn.Opponent;
            }
            else
            {
                // In case of a speed tie, randomly decide
                Random rand = new Random();
                return rand.Next(2) == 0 ? Turn.Player : Turn.Opponent;
            }
        }

        private void SwitchPlayerIdimon(int newIndex)
        {
            if (newIndex >= 0 && newIndex < _player.Count)
            {
                _currentIdimonIndex = newIndex;
            }
        }
    }
}