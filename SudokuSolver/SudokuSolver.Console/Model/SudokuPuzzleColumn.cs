// <copyright file="SudokuPuzzleColumn.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

internal class SudokuPuzzleColumn : SudokuCellCollection
{
    /// <summary>
    /// Index of the column in the puzzle.
    /// 0 is left most column, 9 is rightmost.
    /// </summary>
    public int Index { get; }

    public SudokuPuzzleColumn(List<SudokuPuzzleCell> cells, int positionX)
    {
        this.Cells = new List<SudokuPuzzleCell>();
        this.Index = positionX;
        foreach (var cell in cells)
        {
            cell.AssignParentColumn(this);
            this.Cells.Add(cell);
        }
    }
}