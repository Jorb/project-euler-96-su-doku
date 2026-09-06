// <copyright file="SudokuPuzzleColumn.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// A column element of a sudoku puzzle.
/// </summary>
internal class SudokuPuzzleColumn : SudokuCellCollection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuPuzzleColumn"/> class.
    /// </summary>
    /// <param name="cells">The collection of cells that are contained by this column.</param>
    /// <param name="index">Position of the column in the puzzle. 0 is the leftmost, 8 is the rightmost.</param>
    public SudokuPuzzleColumn(List<SudokuPuzzleCell> cells, int index)
    {
        this.Index = index;
        foreach (var cell in cells)
        {
            cell.AssignParentColumn(this);
            this.Cells.Add(cell);
        }
    }

    /// <summary>
    /// Index of the column in the puzzle.
    /// 0 is left most column, 9 is rightmost.
    /// </summary>
    public int Index { get; }
}