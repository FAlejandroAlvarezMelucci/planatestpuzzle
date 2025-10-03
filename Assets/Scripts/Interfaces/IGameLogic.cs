using System;

namespace PuzzleTest.Interfaces
{
    public interface IGameLogic
    {
        event Action OnNoMoreMoves;
        event Action OnMoveCompleted;
        
        void ResetGame();
        void MakeMove(int posX, int posY);

    }
}