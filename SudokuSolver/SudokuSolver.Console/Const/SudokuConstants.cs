// <copyright file="SudokuConstants.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Console.Const
{
    internal static class SudokuConstants
    {
        internal const int MaxValue = 9;
        internal const int MinValue = 1;
        internal const int BoxRowsPerPuzzle = 3;
        internal const int BoxColumnsPerPuzzle = 3;
        internal const int BoxInnerRows = 3;
        internal const int BoxInnerColumns = 3;

        /// <summary>
        /// The file containing the sudoku puzzle uses 0 to signify empty.
        /// Internal logic uses null.
        /// </summary>
        internal const string FileEmptyCell = "0";

        public static int MaxBoxes = 9;
    }
}
