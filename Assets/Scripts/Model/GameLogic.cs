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

        public GameLogic(IGameSettings settings)
        {
            _settings = settings;

            _visited = new bool[settings.GridWidth, settings.GridHeight];
        }

        public void ResetGame()
        {
            Score = 0;
            Moves = _settings.StartingMoves;
        }

        public void MakeMove(int posX, int posY)
        {
            if (!IsValidMove(posX, posY)) return;

            int collectedBlocksCount = GetConnectedBlocks(posX, posY);

            if (collectedBlocksCount < 2) return;

            Moves--;
            Score += CalculateScore(collectedBlocksCount);
            
            OnMoveCompleted?.Invoke();
        }

        private int CalculateScore(int collectedBlocksCount)
        {
            // We can refactor this method to easily change how points are calculated
            // For now the test calculates 1 point for each removed block
            return collectedBlocksCount;
        }

        /// <summary>
        /// Identifies and counts all connected blocks of the same color starting from the given position using BFS (Breadth First Search).
        /// </summary>
        private int GetConnectedBlocks(int posX, int posY)
        {
            // TODO: Implement Breadth-first search

            return _collectedBlocks.Count;
        }

        private bool IsValidMove(int posX, int posY)
        {
            // check we have moves left and positions are inside the grid
            // TODO add grid check
            return Moves > 0;
        }
    }
}