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

    public bool IsComplete { get => CellListValidationHelper.AreCellsCompleted(Cells); }

    public bool IsValid { get => CellListValidationHelper.AreCellsValid(Cells); }

    public bool IsCompleteAndValid { get => IsComplete && IsValid; }
}