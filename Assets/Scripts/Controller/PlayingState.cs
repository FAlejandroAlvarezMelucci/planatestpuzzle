using PuzzleTest.Interfaces;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Controller
{
    public class PlayingState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly IGameLogic _gameLogic;

        public PlayingState(GameManager gameManager, IGameLogic gameLogic)
        {
            _gameManager = gameManager;
            _gameLogic = gameLogic;
        }

        public void Enter()
        {
            _gameLogic.ResetGame();
            _gameLogic.OnNoMoreMoves += _gameManager.EndGame;
        }

        public void Exit()
        {
            _gameLogic.OnNoMoreMoves -= _gameManager.EndGame;
        }
    }
}