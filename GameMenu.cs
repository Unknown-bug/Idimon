using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Idimon
{
    public class GameMenu : Menu
    {
        private enum MenuState
        {
            Idimon,
            Inventory,
            Save,
            Load,
            Setting
        }

        private int _selectedIndex;
        private MenuState _currentMenu;
        private InventoryMenu _inventoryMenu;
        Player _player;

        public GameMenu(Player player, Window window) : base(window)
        {
            _menuItems = new List<MenuItem>
            {
                new MenuItem("Idimon", 100, 100),
                new MenuItem("Inventory", 100, 140),
                new MenuItem("Save", 100, 180),
                new MenuItem("Load", 100, 220),
                new MenuItem("Setting", 100, 260)
            };
            _player = player;
            _selectedIndex = 0;
            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
            _inventoryMenu = new InventoryMenu(_player, _window);
        }

        public void Navigate(KeyCode key)
        {
            if (!_visible) return;
            if (_inventoryMenu.Visible) return;

            _menuItems[_selectedIndex].IsSelected = false;

            if (key == KeyCode.DownKey)
            {
                _selectedIndex = (_selectedIndex + 1) % _menuItems.Count;
            }
            else if (key == KeyCode.UpKey)
            {
                _selectedIndex = (_selectedIndex - 1 + _menuItems.Count) % _menuItems.Count;
            }

            _menuItems[_selectedIndex].IsSelected = true;
            _currentMenu = (MenuState)_selectedIndex;
        }

        public void Select()
        {
            if (!_visible) return;
            if (_inventoryMenu.Visible) return;

            switch (_currentMenu)
            {
                case MenuState.Idimon:
                    // Idimon logic
                    break;
                case MenuState.Inventory:
                    // Inventory logic
                    _inventoryMenu.Open();
                    // Open();
                    break;
                case MenuState.Save:
                    // Save game logic
                    break;
                case MenuState.Load:
                    // Load game logic
                    break;
                case MenuState.Setting:
                    // Setting logic
                    break;
            }
        }

        public override void Draw()
        {
            if (!_visible) return;
            if(_inventoryMenu.Visible)
            {
                if(SplashKit.KeyTyped(KeyCode.XKey))
                {
                    _inventoryMenu.Toggle();
                    // Toggle();
                }
                _inventoryMenu.Draw();
                return;
            }
            else
            {
                SplashKit.FillRectangle(Color.RGBAColor(0, 0, 0, 125), 0, 0, SplashKit.ScreenWidth(), SplashKit.ScreenHeight());
                foreach (var item in _menuItems)
                {
                    item.Draw();
                }
            }
        }

        public override void HandleInput()
        {
            if (_inventoryMenu.Visible)
            {
                _inventoryMenu.HandleInput();
                return;
            }

            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                Toggle();
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            else if (SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                Select();
            }
        }

        public override void Open()
        {
            Toggle();
        }
    }
}
