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

        public void MakeMove()
        {
            if (Moves <= 0) return;
            
            Moves--;
            Score += 10; // Hardcoded for Task 2
        }
    }
}