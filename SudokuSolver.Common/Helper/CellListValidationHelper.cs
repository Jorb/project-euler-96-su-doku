// <copyright file="CellListValidationHelper.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Common.Helper
{
    using SudokuSolver.Common.Const;

    /// <summary>
    /// Helpers for validating sudoku puzzle cells.
    /// Check if cells are completed and valid for rows, columns, boxes.
    /// </summary>
    internal static class CellListValidationHelper
    {
        /// <summary>
        /// Only Checks for completeness, not validity.
        /// All cells must have a non null value.
        /// </summary>
        /// <param name="cells">The collection of cells to validate.</param>
        /// <returns>The collection of cells to check.</returns>
        internal static bool AreCellsCompleted(List<SudokuPuzzleCell> cells)
        {
            return !cells.Any(cell => cell.CurrentValue is null);
        }

        /// <summary>
        /// Validate that there are no duplicates in a collection of cells.
        /// </summary>
        /// <param name="cells">The collection of cells to validate.</param>
        /// <returns>True if valid.</returns>
        internal static bool AreCellsValid(List<SudokuPuzzleCell> cells)
        {
            var valid = true;
            for (int validNumber = SudokuConstants.MinValue; validNumber <= SudokuConstants.MaxValue; validNumber++)
            {
                //Check if any numbers are duplicated.
                var duplicates = cells.Where(cell => cell.CurrentValue == validNumber).ToList();
                if (duplicates.Count() > 1)
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }
    }
}
