using PuzzleTest.Interfaces;
using PuzzleTest.View;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Controller
{
    public class GameManager : MonoBehaviour, IUIEvents
    {
        private UIManager _uiManager;
        private IGameLogic _gameLogic;

        // Note: More states can be added, like animationState for blocking user input while blocks are falling or exploding
        // Other example states could be: mainMenuState, shopState, profileState, and so on
        private IGameState _currentState;
        private IGameState _playingState;
        private IGameState _gameOverState;

        public void Initialize(UIManager uiManager, IGameLogic gameLogic)
        {
            _uiManager = uiManager;
            _gameLogic = gameLogic;

            _playingState = new PlayingState(gameManager: this, _gameLogic);
            _gameOverState = new GameOverState(_uiManager);

            _gameLogic.OnNoMoreMoves += EndGame;
        }

        private void OnDestroy()
        {
            if (_gameLogic == null) return;

            _gameLogic.OnNoMoreMoves -= EndGame;
        }

        public void StartGame()
        {
            TransitionToState(_playingState);
        }

        public void MakeMove(int x, int y)
        {
            if (_currentState is PlayingState)
                _gameLogic.MakeMove(x, y);
        }

        public void EndGame()
        {
            TransitionToState(_gameOverState);
        }

        private void TransitionToState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}