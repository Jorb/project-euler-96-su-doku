// <copyright file="BackTrackingSudokuPuzzleSolver.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;
using SudokuSolver.Console.Helper;

internal class BackTrackingSudokuPuzzleSolver : ISudokuPuzzleSolver
{
    public void SolvePuzzle(SudokuPuzzle puzzle)
    {
        var currentCellState = CurrentCellStateEnum.Unknown;
        for (int i = 0; i < puzzle.Cells.Count; i++)
        {
            SudokuPuzzleCell cell = puzzle.Cells[i];
            if (cell.IsSettable)
            {
                if (cell.IsSet)
                {
                    if (cell.CurrentValue == SudokuConstants.MaxValue)
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
                    cell.CurrentValue = SudokuConstants.MinValue;
                    currentCellState = CurrentCellStateEnum.Initialized;
                }

                //PrintPuzzle(puzzle);
            }

            i = GetNextCellIndex(cell, i, currentCellState);
        }
    }

    /// <summary>
    /// Determines the next cell to modify based on the state and validity of the current cell.
    /// </summary>
    /// <param name="cell">Cell that was just modified.</param>
    /// <param name="currentCellIndex">Position of the cell in the puzzle.</param>
    /// <param name="currentCellState">Modification state of the current cell.</param>
    /// <returns>The index of the next cell to modify. Remember that this gets incremented by 1 at the start of the loop.</returns>
    /// <exception cref="OutsideOfPuzzleCellBoundsException">Thrown if an invalid cell position is selected.</exception>
    private int GetNextCellIndex(SudokuPuzzleCell cell, int currentCellIndex, CurrentCellStateEnum currentCellState)
    {
        var nextCellIndex = currentCellIndex;
        if (!cell.IsSetAndValid())
        {
            if (currentCellState == CurrentCellStateEnum.Overflowed)
            {
                // Go back to the previous cell and modify it.
                nextCellIndex -= 2;

                if (nextCellIndex < -1)
                {
                    throw new OutsideOfPuzzleCellBoundsException();
                }
            }
            else if (currentCellState == CurrentCellStateEnum.Incremented ||
                currentCellState == CurrentCellStateEnum.Initialized)
            {
                // Increment this cell again.
                nextCellIndex--;
            }
        }
        else if (!cell.IsSettable && currentCellState == CurrentCellStateEnum.Overflowed)
        {
            // Go back to the previous cell and increment it.
            nextCellIndex -= 2;
        }

        return nextCellIndex;
    }

    public void SolvePuzzles(List<SudokuPuzzle> puzzleList)
    {
        for (int puzzleIndex = 0; puzzleIndex < puzzleList.Count; puzzleIndex++)
        {
            Console.Write($"Solving puzzle {puzzleIndex + 1}...");
            this.SolvePuzzle(puzzleList[puzzleIndex]);
            Console.WriteLine("SOLVED");
        }
    }

    private static void PrintPuzzle(SudokuPuzzle puzzle)
    {
        Console.Clear();
        var puzzleLines = PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(0, puzzle);
        foreach (var puzzleLine in puzzleLines) { Console.WriteLine(puzzleLine); }
        Thread.Sleep(100);
    }
}