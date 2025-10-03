using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Interfaces
{
    // NOTE: This interface allows getting the game settings from different sources in the future.
    // For this test I created a ScriptableObject implementation, but it's straightforward to add other sources like web request, JSON files or more
    public interface IGameSettings
    {
        int GridWidth { get; }
        int GridHeight { get; }
        int StartingMoves { get; }
        
        IReadOnlyList<Sprite> BlockColors { get; }
        float GravityDelaySeconds { get; }
    }
}