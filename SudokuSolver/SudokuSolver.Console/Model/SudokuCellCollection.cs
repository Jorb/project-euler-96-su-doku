// <copyright file="SudokuCellCollection.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Helper;

internal class SudokuCellCollection
{
    /// <summary>
    /// The cells that make up the row.
    /// </summary>
    public List<SudokuPuzzleCell> Cells { get; protected set; }

    public bool IsComplete { get => CellListValidationHelper.AreCellsCompleted(this.Cells); }

    public bool IsValid { get => CellListValidationHelper.AreCellsValid(this.Cells); }

    public bool IsCompleteAndValid { get => this.IsComplete && this.IsValid; }
}