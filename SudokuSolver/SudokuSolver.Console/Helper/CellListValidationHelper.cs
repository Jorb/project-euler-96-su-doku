using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Console.Helper
{
    internal static class CellListValidationHelper
    {
        /// <summary>
        /// Only Checks for completeness, not validity.
        /// All cells must have a non null value.
        /// </summary>
        /// <param name="cells"></param>
        /// <returns>The collection of cells to check.</returns>
        internal static bool AreCellsCompleted(List<SudokuPuzzleCell> cells)
        {
            return !cells.Any(cell => cell.CurrentValue is null);
        }

        /// <summary>
        /// Validate that there are no duplicates in a collection of cells
        /// </summary>
        /// <param name="cells">The collection of cells to validate.</param>
        /// <returns>True if valid.</returns>
        internal static bool AreCellsValid(List<SudokuPuzzleCell> cells)
        {
            var valid = true;
            // I should probably put the valid numbers array somewhere else, but the cell is the source of truth.
            foreach (var validNumber in cells[0].ValidNumbers)
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
