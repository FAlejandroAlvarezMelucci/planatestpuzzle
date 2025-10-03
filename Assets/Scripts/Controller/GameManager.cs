using PuzzleTest.Interfaces;
using PuzzleTest.View;
using UnityEngine;

namespace PuzzleTest.Controller
{
    public class GameManager : MonoBehaviour, IUIEvents
    {
        private UIManager _uiManager;
        private IGameLogic _gameLogic;
        
        public void Initialize(UIManager uiManager, IGameLogic gameLogic)
        {
            _uiManager = uiManager;
            _gameLogic = gameLogic;
            
            _gameLogic.OnNoMoreMoves += EndGame;
        }
        
        private void OnDestroy()
        {
            if (_gameLogic == null) return;

            _gameLogic.OnNoMoreMoves -= EndGame;
        }

        public void StartGame()
        {
            _uiManager.ShowGameOverView(showView: false);
            _gameLogic.ResetGame();
        }
        
        private void EndGame()
        {
            _uiManager.ShowGameOverView(showView: true);
        }

        public void MakeMove()
        {
            _gameLogic.MakeMove();
        }
    }
}