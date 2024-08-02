using SplashKitSDK;

namespace Idimon
{
    public class Battle
    {
        private List<Idimons> _playerTeam;
        private List<Idimons> _opponentTeam;
        private Idimons _currentPlayerIdimon;
        private Idimons _currentOpponentIdimon;
        private BattleScreen _battleScreen;
        private bool _isPlayerTurn;

        public Battle(List<Idimons> playerTeam, List<Idimons> opponentTeam, BattleScreen battleScreen)
        {
            _playerTeam = playerTeam;
            _opponentTeam = opponentTeam;
            _battleScreen = battleScreen;
            _currentPlayerIdimon = _playerTeam[0];
            _currentOpponentIdimon = _opponentTeam[0];
            _isPlayerTurn = _currentPlayerIdimon.Speed >= _currentOpponentIdimon.Speed;
        }

        public void Start()
        {
            _battleScreen.Draw();
            _battleScreen.HandleInput();
        }

        public void PlayerAction(int actionIndex)
        {
            switch (actionIndex)
            {
                case 0: // Skills
                    // Use skill (assuming a skill index is chosen by the player)
                    _currentPlayerIdimon.UseMove(0, _currentOpponentIdimon); // Example skill index
                    break;
                case 1: // Switch
                    // Switch Idimon logic
                    SwitchPlayerIdimon(1); // Example switch to second Idimon
                    break;
                case 2: // Items
                    // Use item logic
                    break;
                case 3: // Run
                    // Exit battle
                    _battleScreen.ExitBattle();
                    break;
            }
            _isPlayerTurn = false;
        }

        private void SwitchPlayerIdimon(int newIndex)
        {
            if (newIndex >= 0 && newIndex < _playerTeam.Count)
            {
                _currentPlayerIdimon = _playerTeam[newIndex];
            }
        }

        public void OpponentAction()
        {
            // Opponent AI logic to select a move or action
            _currentOpponentIdimon.UseMove(0, _currentPlayerIdimon); // Example AI move
            _isPlayerTurn = true;
        }

        public void ExecuteTurn()
        {
            if (_isPlayerTurn)
            {
                // Wait for player input
            }
            else
            {
                OpponentAction();
            }

            CheckBattleState();
        }

        private void CheckBattleState()
        {
            if (_currentPlayerIdimon.IsFainted())
            {
                // Handle player Idimon fainted logic
            }

            if (_currentOpponentIdimon.IsFainted())
            {
                // Handle opponent Idimon fainted logic
                _currentPlayerIdimon.GainExperience(100); // Example experience gain
            }
        }
    }
}