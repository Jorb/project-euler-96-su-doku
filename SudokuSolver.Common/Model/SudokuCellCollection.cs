// <copyright file="SudokuCellCollection.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Helper;

/// <summary>
/// A row, column and box are all just collections of cells.
/// The logic for validating all of them is the same, so it can be abstracted out.
/// </summary>
internal class SudokuCellCollection
{
    /// <summary>
    /// Gets the cells that make up the row.
    /// </summary>
    public List<SudokuPuzzleCell> Cells { get; } = new List<SudokuPuzzleCell>();

    /// <summary>
    /// Gets a value indicating whether all the cells have been entered.
    /// </summary>
    public bool IsComplete { get => CellListValidationHelper.AreCellsCompleted(this.Cells); }

    /// <summary>
    /// Gets a value indicating whether all the values are valid (no duplicates).
    /// This is still true if not all cells are complete. Empty cells are technically valid.
    /// </summary>
    public bool IsValid { get => CellListValidationHelper.AreCellsValid(this.Cells); }

    /// <summary>
    /// Gets a value indicating whether all cells are completed (contain a number from 1 to 9) and all of the values are valid (no duplicates.)
    /// </summary>
    public bool IsCompleteAndValid { get => this.IsComplete && this.IsValid; }

    internal List<int> GetPossibleValues()
    {
        List<int> possibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        foreach (var cell in this.Cells)
        {
            if (cell.IsSet)
            {
                possibleValues.Remove((int)cell.CurrentValue);
            }
        }

        return possibleValues;
    }
}