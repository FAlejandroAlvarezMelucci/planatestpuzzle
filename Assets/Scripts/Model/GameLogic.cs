using System;
using PuzzleTest.Interfaces;

namespace PuzzleTest.Model
{
    public class GameLogic : IGameLogic, IGameData
    {
        private int _moves;
        private int _score;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnMovesChanged;
        public event Action OnMoveCompleted;
        public event Action OnNoMoreMoves;

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

        public GameLogic()
        {
            ResetGame();
        }
        
        public void ResetGame()
        {
            Score = 0;
            Moves = 5; // Hardcoded for task 2
        }

        public void MakeMove(int posX, int posY)
        {
            if (!IsValidMove(posX, posY)) return;

            Moves--;
            Score += CalculateScore(1);
            
            OnMoveCompleted?.Invoke();
        }
        
        private int CalculateScore(int collectedBlocksCount)
        {
            // We can refactor this method to easily change how points are calculated
            // For now the test calculates 1 point for each removed block
            return collectedBlocksCount;
        }
        
        private bool IsValidMove(int posX, int posY)
        {
            // check we have moves left and positions are inside the grid
            // TODO add grid check
            return Moves > 0;
        }
    }
}