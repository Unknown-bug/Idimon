using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Idimon
{
    public class GameMenu
    {
        private List<MenuItem> _menuItems;
        private bool _visible;
        private int _selectedIndex;

        public GameMenu()
        {
            _menuItems = new List<MenuItem>
            {
                new MenuItem("Idimon", 100, 100),
                new MenuItem("Item", 100, 140),
                new MenuItem("Save", 100, 180),
                new MenuItem("Load", 100, 220),
                new MenuItem("Setting", 100, 260)
            };
            _visible = false;
            _selectedIndex = 0;
            _menuItems[_selectedIndex].IsSelected = true;
        }

        public bool Visible
        {
            get { return _visible; }
        }

        public void Toggle()
        {
            _visible = !_visible;
        }

        public void Navigate(KeyCode key)
        {
            if (!_visible) return;

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
        }

        public void Select()
        {
            if (!_visible) return;

            MenuItem selectedItem = _menuItems[_selectedIndex];
            // Implement action for each menu item here
            if (selectedItem.Text == "Save")
            {
                // Save game logic
            }
            else if (selectedItem.Text == "Load")
            {
                // Load game logic
            }
            // Add logic for other menu items as needed
        }

        public void Draw()
        {
            if (!_visible) return;
            
            SplashKit.FillRectangle(Color.RGBAColor(0, 0, 0, 125), 0, 0, SplashKit.ScreenWidth(), SplashKit.ScreenHeight());
            foreach (var item in _menuItems)
            {
                item.Draw();
            }
        }

        
    }
}
