// <copyright file="SudokuPuzzle.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;

/// <summary>
/// A Sudoku puzzle. 9X9 with 9 boxes.
/// </summary>
internal class SudokuPuzzle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuPuzzle"/> class.
    /// Build a sudoku puzzle object from raw string lines.
    /// </summary>
    /// <param name="puzzleLines">String lines read from file.</param>
    public SudokuPuzzle(int id, List<string> puzzleLines)
    {
        this.Id = id;
        this.Rows = new List<SudokuPuzzleRow>();
        this.Columns = new List<SudokuPuzzleColumn>();
        this.Boxes = new List<SudokuPuzzleBox>();
        this.Cells = new List<SudokuPuzzleCell>();

        // Start by creating all the rows. Rows are the source of truth.
        for (int positionY = 0; positionY < puzzleLines.Count; positionY++)
        {
            string line = puzzleLines[positionY];
            var newRow = new SudokuPuzzleRow(line, positionY);
            this.Rows.Add(newRow);
            this.Cells.AddRange(newRow.Cells);
        }

        // Link the rows to columns
        for (int xIndex = 0; xIndex < SudokuConstants.MaxValue; xIndex++)
        {
            var columnCells = new List<SudokuPuzzleCell>();
            foreach (var row in this.Rows)
            {
                columnCells.Add(row.Cells[xIndex]);
            }

            this.Columns.Add(new SudokuPuzzleColumn(columnCells, xIndex));
        }

        // Link the rows and columns to boxes
        //Build the box objects.
        for (int boxIndex = 0; boxIndex < SudokuConstants.BoxRowsPerPuzzle * SudokuConstants.BoxColumnsPerPuzzle; boxIndex++)
        {
            int columnIndex = boxIndex / 3;
            int rowIndex = boxIndex % 3;
            this.Boxes.Add(new SudokuPuzzleBox(columnIndex, rowIndex));
        }

        foreach (var row in this.Rows)
        {
            foreach (var cell in row.Cells)
            {
                this.AppendCellToCorrespondingBox(cell);
            }
        }
    }

    /// <summary>
    /// Gets the Id of the puzzle. Corresponds the the grid number in the file.
    /// </summary>
    internal int Id { get; }

    /// <summary>
    /// Gets the 9 rows of the puzzle.
    /// index 0 is the topmost. index 8 is the bottom.
    /// </summary>
    internal List<SudokuPuzzleRow> Rows { get; }

    /// <summary>
    /// Gets the 9 columns of the puzzle.
    /// index 0 is the leftmost. 8 is the rightmost.
    /// </summary>
    internal List<SudokuPuzzleColumn> Columns { get; }

    /// <summary>
    /// Gets the 9 boxes of the puzzle.
    /// index 0 is the top-leftmost. 8 is the bottom-rightmost.
    /// </summary>
    internal List<SudokuPuzzleBox> Boxes { get; }

    /// <summary>
    /// Gets all of the cells that make up the puzzle.
    /// index 0 is the top-leftmost. 80 is the bottom-rightmost.
    /// </summary>
    internal List<SudokuPuzzleCell> Cells { get; }

    /// <summary>
    /// Gets a box by its row and column index (0-2).
    /// </summary>
    /// <param name="boxColumn">index of the box column. 0 to 2.</param>
    /// <param name="boxRow">index of the box row. 0 to 2.</param>
    /// <returns>The box at the specified row-column position.</returns>
    internal SudokuPuzzleBox GetBox(int boxColumn, int boxRow)
    {
        return this.Boxes.Single(box => box.BoxColumn == boxColumn && box.BoxRow == boxRow);
    }

    /// <summary>
    /// Pass the row and column coordinates of any cell in the puzzle and get the box that contains it.
    /// </summary>
    /// <param name="puzzleRow">The row of the cell.</param>
    /// <param name="puzzleColumn">The column of the cell.</param>
    /// <returns>The box that contains the cell with the specified coordinates.</returns>
    internal SudokuPuzzleBox GetBoxFromRawCellCoordinates(int puzzleRow, int puzzleColumn)
    {
        var boxRow = puzzleRow / SudokuConstants.BoxRowsPerPuzzle;
        var boxColumn = puzzleColumn / SudokuConstants.BoxColumnsPerPuzzle;
        return this.GetBox(boxColumn, boxRow);
    }

    /// <summary>
    /// Add a cell to a box.
    /// This will associate the cell with the box so it can be properly validated.
    /// </summary>
    /// <param name="cell">Puzzle cell to add to box.</param>
    private void AppendCellToCorrespondingBox(SudokuPuzzleCell cell)
    {
        if (cell.ParentColumn is not null)
        {
            var boxRow = cell.ParentRow.Index / SudokuConstants.BoxRowsPerPuzzle;
            var boxColumn = cell.ParentColumn.Index / SudokuConstants.BoxColumnsPerPuzzle;
            this.GetBox(boxColumn, boxRow).AppendCell(cell);
        }
        else
        {
            throw new CellParentsNotInitializedException();
        }
    }
}