using System;
using System.Collections.Generic;
using PuzzleTest.Interfaces;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Model
{
    // Note: For simplicity, I implement both interfaces here, since the data is tightly bounded to the logic,
    // if the project grows, it's straightforward to refactor and separate the data to a dedicated class
    public class GameLogic : IGameLogic, IGameData
    {
        private readonly IGridModel _gridModel;
        private readonly IGameSettings _settings;

        private int _moves;
        private int _score;

        private struct Position
        {
            public int X, Y;
        }

        private readonly List<Position> _collectedBlocks = new();
        private readonly Queue<Position> _bfsQueue = new();
        private readonly bool[,] _visited;

        public event Action<int> OnMovesChanged;
        public event Action OnMoveCompleted;
        public event Action OnNoMoreMoves;
        public event Action<int> OnScoreChanged;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke(_score);
            }
        }

        public int Moves
        {
            get => _moves;
            private set
            {
                if (_moves == value) return;

                _moves = value;
                OnMovesChanged?.Invoke(_moves);

                if (_moves <= 0)
                {
                    OnNoMoreMoves?.Invoke();
                }
            }
        }

        public GameLogic(IGameSettings settings, IGridModel gridModel)
        {
            _settings = settings;
            _gridModel = gridModel;

            _visited = new bool[settings.GridWidth, settings.GridHeight];
        }

        public void ResetGame()
        {
            Score = 0;
            Moves = _settings.StartingMoves;

            _gridModel.GenerateGrid();
        }

        public void MakeMove(int posX, int posY)
        {
            if (!IsValidMove(posX, posY)) return;

            int collectedBlocksCount = GetConnectedBlocks(posX, posY);

            if (collectedBlocksCount < 2) return;

            Moves--;
            Score += CalculateScore(collectedBlocksCount);

            foreach (var pos in _collectedBlocks)
            {
                _gridModel.SetBlockColor(pos.X, pos.Y, BlockConstants.EMPTY_BLOCK);
            }

            OnMoveCompleted?.Invoke();
        }

        private int CalculateScore(int collectedBlocksCount)
        {
            // We can refactor this method to easily change how points are calculated
            // For now the test calculates 1 point for each removed block
            return collectedBlocksCount;
        }
        
        public void ApplyGravityAndRefill()
        {
            for (int x = 0; x < _gridModel.Width; x++)
            {
                ApplyGravityToColumn(x);
            }

            RefillEmptySpaces();
        }
        
        private void RefillEmptySpaces()
        {
            // TODO: These should generate outside the grid and fall instead of instant spawning
            
            int maxColor = _settings.BlockColors.Count;
            Random random = new Random();

            for (int x = 0; x < _gridModel.Width; x++)
            {
                for (int y = 0; y < _gridModel.Height; y++)
                {
                    if (_gridModel.GetBlockColor(x, y) == BlockConstants.EMPTY_BLOCK)
                    {
                        _gridModel.SetBlockColor(x, y, random.Next(0, maxColor));
                    }
                }
            }
        }
        
        private void ApplyGravityToColumn(int column)
        {
            int writeY = 0;

            for (int readY = 0; readY < _gridModel.Height; readY++)
            {
                int blockColor = _gridModel.GetBlockColor(column, readY);
                if (blockColor != BlockConstants.EMPTY_BLOCK)
                {
                    if (readY != writeY)
                    {
                        _gridModel.SetBlockColor(column, writeY, blockColor);
                        _gridModel.SetBlockColor(column, readY, BlockConstants.EMPTY_BLOCK);
                    }

                    writeY++;
                }
            }
        }

        /// <summary>
        /// Identifies and counts all connected blocks of the same color starting from the given position using BFS (Breadth First Search).
        /// </summary>
        private int GetConnectedBlocks(int posX, int posY)
        {
            _collectedBlocks.Clear();
            _bfsQueue.Clear();

            if (!_gridModel.IsValidPosition(posX, posY)) return 0;

            int targetColor = _gridModel.GetBlockColor(posX, posY);
            if (targetColor == BlockConstants.EMPTY_BLOCK) return 0;

            Array.Clear(_visited, 0, _visited.Length);
            _bfsQueue.Enqueue(new Position { X = posX, Y = posY });
            _visited[posX, posY] = true;

            int[] directionX = { 0, 0, 1, -1 }; // Up down right left
            int[] directionY = { 1, -1, 0, 0 }; // Up down right left

            while (_bfsQueue.Count > 0)
            {
                Position currentPos = _bfsQueue.Dequeue();
                _collectedBlocks.Add(currentPos);

                for (int i = 0; i < 4; i++)
                {
                    int neighborX = currentPos.X + directionX[i];
                    int neighborY = currentPos.Y + directionY[i];

                    if (_gridModel.IsValidPosition(neighborX, neighborY) &&
                        !_visited[neighborX, neighborY] &&
                        _gridModel.GetBlockColor(neighborX, neighborY) == targetColor)
                    {
                        _visited[neighborX, neighborY] = true;
                        _bfsQueue.Enqueue(new Position { X = neighborX, Y = neighborY });
                    }
                }
            }

            return _collectedBlocks.Count;
        }

        private bool IsValidMove(int posX, int posY)
        {
            // check we have moves left and positions are inside the grid
            return Moves > 0 && _gridModel.IsValidPosition(posX, posY);
        }
    }
}