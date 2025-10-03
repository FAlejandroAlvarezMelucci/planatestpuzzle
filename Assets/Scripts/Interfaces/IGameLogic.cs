using System;

namespace PuzzleTest.Interfaces
{
    public interface IGameLogic
    {
        event Action OnNoMoreMoves;

        void ResetGame();
        void MakeMove();
    }
}