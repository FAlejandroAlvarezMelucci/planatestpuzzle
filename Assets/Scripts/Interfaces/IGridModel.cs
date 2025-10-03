using System;

// ReSharper disable once CheckNamespace
namespace PuzzleTest.Interfaces
{
    public interface IGridModel
    {
        int Width { get; }
        int Height { get; }

        void GenerateGrid();

        /// <summary>
        /// Sets the color of the block for the given coordinates.
        /// Note that color is an index based on the game's color settings list.
        /// A value of -1 means an empty block.
        /// </summary>
        /// <param name="posX">The x-coordinate of the block.</param>
        /// <param name="posY">The y-coordinate of the block.</param>
        /// <param name="color">The color index of the specified block, where -1 means the block is empty.</param>
        void SetBlockColor(int posX, int posY, int color);

        /// <summary>
        /// Retrieves the color of the block at the specified coordinates.
        /// The color is represented by an index based on the game's color settings list.
        /// A value of -1 indicates an empty block.
        /// </summary>
        /// <param name="posX">The x-coordinate of the block.</param>
        /// <param name="posY">The y-coordinate of the block.</param>
        /// <returns>The color index of the specified block, or -1 if the block is empty.</returns>
        int GetBlockColor(int posX, int posY);

        /// <summary>
        /// Determines whether the specified position is valid within the grid.
        /// </summary>
        /// <param name="posX">The x-coordinate of the position.</param>
        /// <param name="posY">The y-coordinate of the position.</param>
        /// <returns>True if the position is valid within the grid boundaries; otherwise, false.</returns>
        bool IsValidPosition(int posX, int posY);

        event Action<int, int> OnBlockDestroyed;
        event Action OnGridGenerated;
    }
}