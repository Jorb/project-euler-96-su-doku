// <copyright file="SudokuPuzzleBox.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;

internal class SudokuPuzzleBox : SudokuCellCollection
{
    public SudokuPuzzleBox(List<SudokuPuzzleCell> cells, int boxColumn, int boxRow)
    {
        Cells = new List<SudokuPuzzleCell>();
        foreach(var cell in cells)
        {
            cell.AssignParentBox(this);
            Cells.Add(cell);
        }

        BoxColumn = boxColumn;
        BoxRow = boxRow;
    }

    public SudokuPuzzleBox(int boxColumn, int boxRow)
    {
        Cells = new List<SudokuPuzzleCell>();
        BoxColumn = boxColumn;
        BoxRow = boxRow;
    }

    /// <summary>
    /// Index of the box column (0 to 2 since there are 3 columns of boxes per puzzle)
    /// </summary>
    public int BoxColumn { get; }

    /// <summary>
    /// Index of the box row (0 to 2 since there are 3 rows of boxes per puzzle)
    /// </summary>
    public int BoxRow { get; }

    internal List<SudokuPuzzleCell> GetRowValues(int innerBoxRow)
    {
        var rowCells = new List<SudokuPuzzleCell>();
        // Cells are not stored in columns and rows in this object. We have to derive that.
        int startCell = innerBoxRow * SudokuConstants.BoxInnerRows;

        for (int cellIndex = startCell; cellIndex < startCell + SudokuConstants.BoxInnerRows; cellIndex++)
        {
            rowCells.Add(Cells[cellIndex]);
        }

        return rowCells;
    }

    internal void AppendCell(SudokuPuzzleCell cell)
    {
        cell.AssignParentBox(this);
        Cells.Add(cell);
    }
}