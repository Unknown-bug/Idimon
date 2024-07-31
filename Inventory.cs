using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Idimon
{
    public class Inventory
    {
        private Dictionary<string, Items> _items;

        public Inventory()
        {
            _items = new Dictionary<string, Items>();
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
        public List<Items> GetAllItems()
        {
            return new List<Items>(_items.Values);
        }

        // Display the inventory
        public void DisplayInventory(Window window, double startX, double startY)
        {
            double x = startX;
            double y = startY;
            const double ITEM_SPACING = 50;

            foreach (Items item in _items.Values)
            {
                item.Draw(window, x, y);
                y += ITEM_SPACING; // Adjust spacing as needed
            }
        }

        // Get the total number of items
        public int GetTotalItems()
        {
            return _items.Count;
        }
    }
}
