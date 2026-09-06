// <copyright file="SudokuConstants.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Console.Const
{
    /// <summary>
    /// Constants related to a sudoku puzzle.
    /// </summary>
    /// <remarks>
    /// It may seem like a lot of duplicate values, but it definitely makes the associated code more readable.
    /// </remarks>
    internal static class SudokuConstants
    {
        /// <summary>
        /// The max valid value of a sudoku cell.
        /// </summary>
        internal const int MaxValue = 9;

        /// <summary>
        /// The minimum valid value of a sudoku cell.
        /// </summary>
        internal const int MinValue = 1;

        /// <summary>
        /// The number of box rows in the sudoku puzzle.
        /// </summary>
        internal const int BoxRowsPerPuzzle = 3;

        /// <summary>
        /// The number of box columns in the sudoku puzzle.
        /// </summary>
        internal const int BoxColumnsPerPuzzle = 3;

        /// <summary>
        /// The number of rows inside a box.
        /// </summary>
        internal const int BoxInnerRows = 3;

        /// <summary>
        /// The number of columns inside a box.
        /// </summary>
        internal const int BoxInnerColumns = 3;

        /// <summary>
        /// The file containing the sudoku puzzle uses 0 to signify empty.
        /// Internal logic uses null.
        /// </summary>
        internal const string FileEmptyCell = "0";
    }
}
