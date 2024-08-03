using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Idimon
{
    public class Inventory
    {
        private Dictionary<string, Items> _items;
        private List<Idimons> _idimons;
        private int _selectedIndex;
        private bool _visible;

        public Inventory()
        {
            _items = new Dictionary<string, Items>();
            _idimons = new List<Idimons>();
            _selectedIndex = 0;
            _visible = false;
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public void Toggle()
        {
            _visible = !_visible;
        }

        // Add item to the inventory
        public void AddItem(Items item)
        {
            if (_items.ContainsKey(item.Name))
            {
                _items[item.Name].Quantity += item.Quantity;
            }
            else
            {
                _items[item.Name] = item;
            }
        }

        // Add idimon to the inventory
        public void AddIdimon(Idimons idimon)
        {
            if(_idimons.Count < 6)
            {
                _idimons.Add(idimon);
            }
        }

        // Remove one quantity of the item from the inventory
        public void RemoveItem(string itemName)
        {
            if (_items.ContainsKey(itemName))
            {
                _items[itemName].Quantity -= 1;

                if (_items[itemName].Quantity <= 0)
                {
                    _items.Remove(itemName);
                }
            }
        }

        public void RemoveIdimon(Idimons idimon)
        {
            _idimons.Remove(idimon);
        }

        // Find item by name
        public Items FindItemByName(string name)
        {
            if (_items.ContainsKey(name))
            {
                return _items[name];
            }
            return null;
        }

        // Get the list of all items
        public List<Items> GetAllItems(string type)
        {
            List<Items> items = new List<Items>();
            foreach (Items item in _items.Values)
            {
                if (item.Type == type)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        // Display the inventory
        public void DisplayInventory(Window window, double startX, double startY, string type)
        {
            double x = startX;
            double y = startY;

            double X_SPACING = 105 + SplashKit.ScreenWidth() / 3;
            double Y_SPACING = 50;

            List<Items> items = GetAllItems(type);
            int i = 0;

            foreach (Items item in items)
            {
                i += 1;
                item.Draw(window, x, y);
                if (i % 2 != 0)
                {
                    x += X_SPACING;
                }
                else
                {
                    x = startX;
                    y += Y_SPACING;
                }
            }

            // Highlight the selected item
            if (items.Count > 0)
            {
                items[_selectedIndex].IsSelected = true;
            }
        }

        // Navigate through the items in the inventory
        public void Navigate(string type)
        {
            List<Items> items = GetAllItems(type);
            if (items.Count == 0) return;

            // items[_selectedIndex].IsSelected = false;
            if (SplashKit.KeyTyped(KeyCode.RightKey))
            {
                items[_selectedIndex].IsSelected = false;
                _selectedIndex = (_selectedIndex + 1) % items.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                items[_selectedIndex].IsSelected = false;
                _selectedIndex = (_selectedIndex - 1 + items.Count) % items.Count;
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey))
            {
                items[_selectedIndex].IsSelected = false;
                if(_items.Count % 2 != 0 && _selectedIndex == _items.Count - 1)
                {
                    _selectedIndex = (_selectedIndex + 1) % items.Count;
                }
                else
                {
                    _selectedIndex = (_selectedIndex + 2) % items.Count;
                }
            }
            else if (SplashKit.KeyTyped(KeyCode.UpKey))
            {
                items[_selectedIndex].IsSelected = false;
                if(_items.Count % 2 != 0 && _selectedIndex == _items.Count - 1)
                {
                    _selectedIndex = (_selectedIndex - 1 + items.Count) % items.Count;
                }
                else
                {
                    _selectedIndex = (_selectedIndex - 2 + items.Count) % items.Count;
                }
            }
        }

        public string HandleInput(string type)
        {
            List<Items> items = GetAllItems(type);
            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                if (items.Count == 0) 
                {
                    Toggle();
                    SplashKit.Delay(100);
                    return "";
                }
                items[_selectedIndex].IsSelected = false;

                _selectedIndex = 0;
                Toggle();
                SplashKit.Delay(100);
                return "";
            }
            Navigate(type);
            return type;
        }

        // Get the total number of items
        public int GetTotalItems()
        {
            return _items.Count;
        }

        public List<Idimons> Idimons
        {
            get { return _idimons; }
        }
    }
}
