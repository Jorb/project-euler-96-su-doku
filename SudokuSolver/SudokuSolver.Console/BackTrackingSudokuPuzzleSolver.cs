internal class BackTrackingSudokuPuzzleSolver : ISudokuPuzzleSolver
{
    public void SolvePuzzle(SudokuPuzzle puzzle)
    {
        //Start with the first (top left) cell
        for (int rowIndex = 0; rowIndex < puzzle.Rows.Count; rowIndex++)
        {
            var row = puzzle.Rows[rowIndex];
            for (int cellIndex = 0; cellIndex < 9; cellIndex++)
            {
                var cell = row.Cells[cellIndex];
                var currentCellState = CurrentCellStateEnum.Unknown;
                if (cell.IsSettable && cell.IsSet)
                {
                    if (cell.CurrentValue == 9)
                    {
                        //Clear the value and go back to the previous sibling;
                        cell.ClearValue();
                        currentCellState = CurrentCellStateEnum.Overflowed;
                    }
                    else
                    {
                        // Increment if less than 9.
                        cell.CurrentValue++;
                        currentCellState = CurrentCellStateEnum.Incremented;
                    }
                }
                else
                {
                    //Init to 1 if never set.
                    cell.CurrentValue = 1;
                    currentCellState = CurrentCellStateEnum.Initialized;
                }

                if (!cell.IsSetAndValid())
                {
                    switch (currentCellState)
                    {
                        case CurrentCellStateEnum.Overflowed:
                            // Go back to the previous cell and increment it.
                            cellIndex -= 2;
                            break;
                        case CurrentCellStateEnum.Incremented:
                        case CurrentCellStateEnum.Initialized:
                            // Increment this cell again.
                            cellIndex--;
                            break;

                    }
                }


                if (cellIndex < 0)
                {
                    //If all the values failed in this row, go up a row and backtrack further.
                    rowIndex -= 2;
                    break;
                }
            }
        }
    }

    public void SolvePuzzles(List<SudokuPuzzle> puzzleList)
    {
        foreach (var puzzle in puzzleList)
        {
            SolvePuzzle(puzzle);
        }
    }
}