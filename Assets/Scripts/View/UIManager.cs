using System.Globalization;
using NUnit.Framework;
using PuzzleTest.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PuzzleTest.View
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button replayButton;
        [SerializeField] private Button makeMoveButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI movesText;

        private readonly CultureInfo _dotAsThousandSeparator = new("de-DE");
        private IGameData _gameData;
        private IUIEvents _uiEvents;

        private void Awake()
        {
            // Assert initial state is valid, and we didn't forget to assign references
            Assert.IsNotNull(gameplayPanel);
            Assert.IsNotNull(gameOverPanel);
            Assert.IsNotNull(replayButton);
            Assert.IsNotNull(makeMoveButton);
            Assert.IsNotNull(scoreText);
            Assert.IsNotNull(movesText);
        }

        public void Initialize(IGameData gameData, IUIEvents uiEvents)
        {
            _gameData = gameData;
            _uiEvents = uiEvents;

            _gameData.OnScoreChanged += UpdateScoreText;
            _gameData.OnMovesChanged += UpdateMovesText;

            replayButton.onClick.AddListener(_uiEvents.StartGame);
            makeMoveButton.onClick.AddListener(_uiEvents.MakeMove);

            UpdateScoreText(_gameData.Score);
            UpdateMovesText(_gameData.Moves);

            ShowGameOverView(showView: false);
        }

        private void OnDestroy()
        {
            if (_gameData != null)
            {
                _gameData.OnScoreChanged -= UpdateScoreText;
                _gameData.OnMovesChanged -= UpdateMovesText;
            }

            replayButton.onClick.RemoveAllListeners();
            makeMoveButton.onClick.RemoveAllListeners();
        }

        private void UpdateScoreText(int newScore)
        {
            scoreText.text = newScore.ToString(_dotAsThousandSeparator);
        }

        private void UpdateMovesText(int newMoves)
        {
            movesText.text = newMoves.ToString();
        }

        public void ShowGameOverView(bool showView)
        {
            gameOverPanel.SetActive(showView);
        }
    }
}