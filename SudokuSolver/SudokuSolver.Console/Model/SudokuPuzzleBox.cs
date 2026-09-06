// <copyright file="SudokuPuzzleBox.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;

/// <summary>
/// A box element of the sudoku puzzle (3x3 sub grid of the puzzle).
/// </summary>
internal class SudokuPuzzleBox : SudokuCellCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuPuzzleBox"/> class.
    /// </summary>
    /// <param name="boxColumn">The column of the box (0 to 2).</param>
    /// <param name="boxRow">The row of the box (0 to 2).</param>
    public SudokuPuzzleBox(int boxColumn, int boxRow)
    {
        this.BoxColumn = boxColumn;
        this.BoxRow = boxRow;
    }

    /// <summary>
    /// Gets index of the box column (0 to 2 since there are 3 columns of boxes per puzzle).
    /// </summary>
    public int BoxColumn { get; }

    /// <summary>
    /// Gets index of the box row (0 to 2 since there are 3 rows of boxes per puzzle).
    /// </summary>
    public int BoxRow { get; }

    /// <summary>
    /// Get all of the cells from an inner row of the box (3 values per row.)
    /// </summary>
    /// <param name="innerBoxRow">The inner row of the box. A box contains 3 rows.</param>
    /// <returns>A list of the 3 cells in the desired row.</returns>
    internal List<SudokuPuzzleCell> GetInnerRowCells(int innerBoxRow)
    {
        var rowCells = new List<SudokuPuzzleCell>();
        // Cells are not stored in columns and rows in this object. We have to derive that.
        int startCell = innerBoxRow * SudokuConstants.BoxInnerRows;

        for (int cellIndex = startCell; cellIndex < startCell + SudokuConstants.BoxInnerRows; cellIndex++)
        {
            rowCells.Add(this.Cells[cellIndex]);
        }

        return rowCells;
    }

    /// <summary>
    /// Append a cell to this box.
    /// The cells are not stored explicitly as rows and columns, but the rows can be derived (row = cell/3, cell = cell %3).
    /// </summary>
    /// <param name="cell">Cell to append to this box.</param>
    internal void AppendCell(SudokuPuzzleCell cell)
    {
        cell.AssignParentBox(this);
        this.Cells.Add(cell);
    }
}