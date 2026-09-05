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
                    if (cell.IsSet)
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
                }

                if (!cell.IsSetAndValid())
                {

                    if (currentCellState == CurrentCellStateEnum.Overflowed)
                    {
                        // Go back to the previous cell and increment it.
                        cellIndex -= 2;

                        if (cellIndex < 0)
                        {
                            rowIndex -= 2;
                        }
                        break;
                    }
                    else if (currentCellState == CurrentCellStateEnum.Incremented ||
                        currentCellState == CurrentCellStateEnum.Initialized)
                    {
                        // Increment this cell again.
                        cellIndex--;
                    }

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