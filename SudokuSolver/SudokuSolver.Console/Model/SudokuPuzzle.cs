// <copyright file="SudokuPuzzle.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;

internal class SudokuPuzzle : ISudokuPuzzle
{
    public List<SudokuPuzzleRow> Rows;
    public List<SudokuPuzzleColumn> Columns;
    public List<SudokuPuzzleBox> Boxes;
    public List<SudokuPuzzleCell> Cells { get; internal set; }

    /// <summary>
    /// Build a sudoku puzzle object from raw string lines.
    /// </summary>
    /// <param name="puzzleLines">String lines read from file.</param>
    public SudokuPuzzle(List<string> puzzleLines)
    {
        Rows = new List<SudokuPuzzleRow>();
        Columns = new List<SudokuPuzzleColumn>();
        Boxes = new List<SudokuPuzzleBox>();
        Cells = new List<SudokuPuzzleCell>();

        // Start by creating all the rows. Rows are the source of truth.
        for (int positionY = 0; positionY < puzzleLines.Count; positionY++)
        {
            string line = puzzleLines[positionY];
            var newRow = new SudokuPuzzleRow(line, positionY);
            Rows.Add(newRow);
            Cells.AddRange(newRow.Cells);
        }

        // Link the rows to columns
        for (int xIndex = 0; xIndex < SudokuConstants.MaxValue; xIndex++)
        {
            var columnCells = new List<SudokuPuzzleCell>();
            foreach (var row in Rows)
            {
                columnCells.Add(row.Cells[xIndex]);
            }

            Columns.Add(new SudokuPuzzleColumn(columnCells, xIndex));
        }



        // Link the rows and columns to boxes
        //Build the box objects.
        for (int boxIndex = 0; boxIndex < SudokuConstants.BoxRowsPerPuzzle * SudokuConstants.BoxColumnsPerPuzzle; boxIndex++)
        {
            int columnIndex = boxIndex / 3;
            int rowIndex = boxIndex % 3;
            Boxes.Add(new SudokuPuzzleBox(columnIndex, rowIndex));
        }

        foreach (var row in Rows)
        {
            foreach (var cell in row.Cells)
            {
                AddCellToCorrespondingBox(cell);
            }
        }
    }

    private void AddCellToCorrespondingBox(SudokuPuzzleCell cell)
    {
        var boxRow = cell.ParentRow.Index / SudokuConstants.BoxRowsPerPuzzle;
        var boxColumn = cell.ParentColumn.Index / SudokuConstants.BoxColumnsPerPuzzle;
        GetBox(boxColumn, boxRow).AppendCell(cell);
    }

    internal SudokuPuzzleBox GetBox(int boxColumn, int boxRow)
    {
        return Boxes.Single(box => box.BoxColumn == boxColumn && box.BoxRow == boxRow);
    }

    internal SudokuPuzzleBox GetBoxFromRawCellCoordinates(int puzzleRow, int puzzleColumn)
    {
        var boxRow = puzzleRow / SudokuConstants.BoxRowsPerPuzzle;
        var boxColumn = puzzleColumn / SudokuConstants.BoxColumnsPerPuzzle;
        return GetBox(boxColumn, boxRow);
    }
}