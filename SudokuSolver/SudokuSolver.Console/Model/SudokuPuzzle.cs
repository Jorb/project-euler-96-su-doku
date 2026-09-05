using SudokuSolver.Console.Const;

internal class SudokuPuzzle : ISudokuPuzzle
{
    public List<SudokuPuzzleRow> Rows;
    public List<SudokuPuzzleColumn> Columns;
    public List<SudokuPuzzleBox> Boxes;

    /// <summary>
    /// Build a sudoku puzzle object from raw string lines.
    /// </summary>
    /// <param name="puzzleLines">String lines read from file.</param>
    public SudokuPuzzle(List<string> puzzleLines)
    {
        Rows = new List<SudokuPuzzleRow>();
        Columns = new List<SudokuPuzzleColumn>();
        Boxes = new List<SudokuPuzzleBox>();

        // Start by creating all the rows
        for (int positionY = 0; positionY < puzzleLines.Count; positionY++)
        {
            string line = puzzleLines[positionY];
            Rows.Add(new SudokuPuzzleRow(line, positionY));
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

        int boxWidth = 3;
        // Link the rows and columns to boxes
        for (int boxXIndex = 0; boxXIndex < 3; boxXIndex++)
        {
            for (int boxYIndex = 0; boxYIndex < 3; boxYIndex++)
            {
                var boxCells = new List<SudokuPuzzleCell>();
                for (int rowIndex = boxXIndex * 3; rowIndex < boxXIndex * 3 + 3; rowIndex++)
                {
                    for(int columnIndex = boxYIndex * 3; columnIndex < boxYIndex * 3 + 3; columnIndex++)
                    {
                        boxCells.Add(Rows[rowIndex].Cells[columnIndex]);
                    }
                }
                Boxes.Add(new SudokuPuzzleBox(boxCells, boxXIndex, boxYIndex));
            }
        }
    }

}