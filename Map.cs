using System;
using SplashKitSDK;

namespace Idimon
{
    public class Map
    {
        Window _window;
        private char[,] _mapData;
        private Dictionary<char, Block> _blockTypes;
        private int _blockSize;

        public Map(char[,] mapData, Dictionary<char, Block> blockTypes, Window window, int blockSize = 64)
        {
            _mapData = mapData;
            _blockTypes = blockTypes;
            _blockSize = blockSize;
            _window = window;
        }

        public bool CanMoveTo(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _mapData.GetLength(0) || y >= _mapData.GetLength(1))
            {
                return false;
            }
            return _mapData[x, y] != 'x';
        }

        public void Draw(Player player)
        {
            int screenWidth = _window.Width;
            int screenHeight = _window.Height;

            int playerX = (int)player.Position.X / _blockSize;
            int playerY = (int)player.Position.Y / _blockSize;

            int halfScreenWidth = screenWidth / (2 * _blockSize);
            int halfScreenHeight = screenHeight / (2 * _blockSize);

            int startX = playerX - halfScreenWidth;
            int startY = playerY - halfScreenHeight;

            for (int y = 0; y < screenHeight / _blockSize; y++)
            {
                for (int x = 0; x < screenWidth / _blockSize; x++)
                {
                    int mapX = startX + x;
                    int mapY = startY + y;

                    if (mapX >= 0 && mapY >= 0 && mapX < _mapData.GetLength(0) && mapY < _mapData.GetLength(1))
                    {
                        char blockChar = _mapData[mapX, mapY];
                        if (_blockTypes.ContainsKey(blockChar))
                        {
                            _blockTypes[blockChar].Draw(_window, x * _blockSize, y * _blockSize);
                        }
                    }
                }
            }
            // _player.Draw(_player.X*64, _player.Y*64);
        }
    }
}
