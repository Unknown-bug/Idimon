using SplashKitSDK;

namespace Idimon
{
    public class InventoryMenu : Menu
    {
        private enum MenuState
        {
            Item,
            keyItem
        }
        // Window _window;
        private int _width, _length;
        private Point _point;
        private List<Items> items;
        private Player _player;

        public InventoryMenu(Player player, Window window) : base( window)
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
            items = _player.Inventory.GetAllItems();
        }

        public override void Draw()
        {
            if (!_visible) return;
            // SplashKit.DrawText("Name", Color.Yellow, "Arial", 30, 100 + 5, 200);

            List<Items> items = _player.Inventory.GetAllItems();

            _player.Inventory.DisplayInventory(_window, 100, 200);
            // for (int i = 0; i < items.Count; i++)
            // {
            //     // SplashKit.FillRectangle(Color.RGBAColor(255, 255, 255, 125), x, y, 35, 35);
            //     items[i].Draw(_window, 100, 200 + i * 50);
            // }
        }

        public override void HandleInput()
        {
            if (!_visible) return;

            if (SplashKit.KeyTyped(KeyCode.XKey))
            {
                Toggle();
            }
        }

        public override void Open()
        {
            
            // _window.Refresh(60);
            Toggle();
        }
    }
}