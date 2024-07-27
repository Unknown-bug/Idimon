using System;
using SplashKitSDK;

namespace Idimon
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory()
        {
            _items = new List<Item>();
        }

        public void AddItem(Item i)
        {
            _items.Add(i);
        }

        public void RemoveItem(Item i)
        {
            _items.Remove(i);
        }

        public void Open()
        {
                
        }
    }
}
