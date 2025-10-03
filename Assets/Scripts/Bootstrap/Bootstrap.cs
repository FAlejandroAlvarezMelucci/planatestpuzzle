using PuzzleTest.Controller;
using PuzzleTest.Interfaces;
using PuzzleTest.Model;
using PuzzleTest.View;
using UnityEngine;

namespace PuzzlePractice.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            // Model startup
            IGameLogic gameLogic = new GameLogic();
            IGameData gameData = (IGameData)gameLogic;

            // View startup
            _uiManager.Initialize(gameData, _gameManager);

            // Controllers startup
            _gameManager.Initialize(_uiManager, gameLogic);
            _gameManager.StartGame();
        }
    }
}