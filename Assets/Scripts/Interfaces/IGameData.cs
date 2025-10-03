using System;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Interfaces
{
    public interface IGameData
    {
        event Action<int> OnScoreChanged;
        event Action<int> OnMovesChanged;
        
        int Score { get; }
        int Moves { get; }
    }
}