using System;
using PuzzleTest.Interfaces;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Model
{
    public class GridModel : IGridModel
    {
        private readonly IGameSettings _settings;
        private readonly int[,] _grid;

        public event Action<int, int> OnBlockDestroyed;
        public event Action OnGridGenerated;

        public int Width => _settings.GridWidth;
        public int Height => _settings.GridHeight;

        public GridModel(IGameSettings settings)
        {
            _settings = settings;

            _grid = new int[Width, Height];
        }

        public void GenerateGrid()
        {
            int maxColor = _settings.BlockColors.Count;
            Random random = new Random();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _grid[x, y] = random.Next(0, maxColor);
                }
            }
        }

        public void SetBlockColor(int posX, int posY, int color)
        {
            if (!IsValidPosition(posX, posY)) return;

            if (_grid[posX, posY] != BlockConstants.EMPTY_BLOCK && color == BlockConstants.EMPTY_BLOCK)
            {
                OnBlockDestroyed?.Invoke(posX, posY);
            }

            _grid[posX, posY] = color;
        }

        public int GetBlockColor(int posX, int posY)
        {
            if (!IsValidPosition(posX, posY)) return BlockConstants.EMPTY_BLOCK;

            return 0;
        }

        public bool IsValidPosition(int posX, int posY)
        {
            return posX >= 0 && posX < Width &&
                   posY >= 0 && posY < Height;
        }
    }
}