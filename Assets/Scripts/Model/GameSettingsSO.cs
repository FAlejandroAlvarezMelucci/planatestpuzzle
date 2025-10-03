using System.Collections.Generic;
using PuzzleTest.Interfaces;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Model
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "PuzzleTest/New Game Settings")]
    public class GameSettingsSO : ScriptableObject, IGameSettings
    {
        [SerializeField] private int _gridWidth = 6;
        [SerializeField] private int _gridHeight = 5;
        [SerializeField] private int _startingMoves = 5;
        [SerializeField] private List<Sprite> _blockColors = new();
        [SerializeField] private float _gravityDelaySeconds = 1f;

        public int GridWidth => _gridWidth;
        public int GridHeight => _gridHeight;
        public int StartingMoves => _startingMoves;
        public IReadOnlyList<Sprite> BlockColors => _blockColors;
        public float GravityDelaySeconds => _gravityDelaySeconds;
    }
}