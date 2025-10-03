using PuzzleTest.Controller;
using PuzzleTest.Interfaces;
using PuzzleTest.Model;
using PuzzleTest.View;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace PuzzlePractice.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameSettingsSO gameSettingsAsset;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            IGameSettings gameSettings = gameSettingsAsset;

            // Example of a web request to fetch the settings
            // IGameSettings gameSettings = await webRequestService.FetchSettings();

            // Model startup
            IGridModel gridModel = new GridModel(gameSettings);
            IGameLogic gameLogic = new GameLogic(gameSettings, gridModel);
            IGameData gameData = (IGameData)gameLogic;

            // View startup
            _uiManager.Initialize(gameData, _gameManager);

            // Controllers startup
            _gameManager.Initialize(_uiManager, gameLogic, gameSettings);
            _gameManager.StartGame();
        }
    }
}