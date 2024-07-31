using SplashKitSDK;

namespace Idimon
{
    public class InventoryMenu : Menu
    {
        private enum MenuState
        {
            Items,
            Equipment,
            KeyItems
        }
        // Window _window;
        private int _width, _length;
        private Point _point;
        private List<Items> items;
        private Player _player;
        private int _selectedIndex;
        private MenuState _currentMenu;

        public InventoryMenu(Player player, Window window) : base( window)
        { 
            _menuItems = new List<MenuItem>
            {
                new MenuItem("Items", 92.5 + ((SplashKit.ScreenWidth() - 10) / 3 * 0), 120),
                new MenuItem("Equipment", 92.5 + ((SplashKit.ScreenWidth() - 10) / 3 * 1), 120),
                new MenuItem("Key Items", 92.5 + ((SplashKit.ScreenWidth() - 10) / 3 * 2), 120),
            };
            _selectedIndex = 0;
            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
            _player = player;
            items = _player.Inventory.GetAllItems();
        }

        public void Select()
        {
            if (!_visible) return;

            switch (_currentMenu)
            {
                case MenuState.Items:
                    // Items logic
                    break;
                case MenuState.Equipment:
                    // Equipment logic
                    break;
                case MenuState.KeyItems:
                    // Key Items logic
                    break;
            }
        }

        public override void Draw()
        {
            if (!_visible) return;
            // SplashKit.DrawText("Name", Color.Yellow, "Arial", 30, 100 + 5, 200);
            SplashKit.FillRectangle(Color.RGBAColor(38, 35, 67, 140), 10, 0, SplashKit.ScreenWidth() - 20, 100);
            SplashKit.DrawRectangle(Color.RGBAColor(0, 0, 0, 255), 10, 0, SplashKit.ScreenWidth() - 20, 100);

            SplashKit.FillRectangle(Color.RGBAColor(38, 35, 67, 140), 10, 110, SplashKit.ScreenWidth() - 20, 60);
            SplashKit.DrawRectangle(Color.RGBAColor(0, 0, 0, 255), 10, 110, SplashKit.ScreenWidth() - 20, 60);

            SplashKit.FillRectangle(Color.RGBAColor(38, 35, 67, 140), 10, 180, SplashKit.ScreenWidth() - 20, SplashKit.ScreenHeight() - 185);
            SplashKit.DrawRectangle(Color.RGBAColor(0, 0, 0, 255), 10, 180, SplashKit.ScreenWidth() - 20, SplashKit.ScreenHeight() - 185);
            
            // SplashKit.FillRectangle(Color.RGBAColor(0, 0, 0, 255), 0, 100, SplashKit.ScreenWidth(), 10);

            foreach (var item in _menuItems)
            {
                item.Draw();
            }

            List<Items> items = _player.Inventory.GetAllItems();

            _player.Inventory.DisplayInventory(_window, 100, 200);
            // for (int i = 0; i < items.Count; i++)
            // {
            //     // SplashKit.FillRectangle(Color.RGBAColor(255, 255, 255, 125), x, y, 35, 35);
            //     items[i].Draw(_window, 100, 200 + i * 50);
            // }
        }

        public void Navigate(KeyCode key)
        {
            if (!_visible) return;

            _menuItems[_selectedIndex].IsSelected = false;

            if (key == KeyCode.RightKey)
            {
                _selectedIndex = (_selectedIndex + 1) % _menuItems.Count;
            }
            else if (key == KeyCode.LeftKey)
            {
                _selectedIndex = (_selectedIndex - 1 + _menuItems.Count) % _menuItems.Count;
            }

            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
        }

        public override void HandleInput()
        {
            if (!_visible) return;

            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                Console.WriteLine("Toggle");
                Toggle();
            }
            else if (SplashKit.KeyTyped(KeyCode.RightKey) || SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                // Console.WriteLine("Navigate");
                Navigate(SplashKit.KeyTyped(KeyCode.RightKey) ? KeyCode.RightKey : KeyCode.LeftKey);
            }
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                Select();
            }
        }

        public override void Open()
        {
            
            // _window.Refresh(60);
            Toggle();
        }
    }
}