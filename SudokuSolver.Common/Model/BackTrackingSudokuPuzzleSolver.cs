// <copyright file="BackTrackingSudokuPuzzleSolver.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Const;
using SudokuSolver.Common.Helper;

/// <summary>
/// Sudoku puzzle solver using backtracking algorithm.
/// https://en.wikipedia.org/wiki/Sudoku_solving_algorithms#Backtracking .
/// </summary>
public class BackTrackingSudokuPuzzleSolver : ISudokuPuzzleSolver
{
    /// <summary>
    /// Solve the sudoku puzzle using the backtracking sudoku algorithm.
    /// </summary>
    /// <param name="puzzle">The puzzle to solve.</param>
    /// <param name="showLiveView">Whether or not to show the live view of puzzle solver.</param>
    /// <param name="showPuzzle">The delegate to show the puzzle in the UI.</param>
    public void SolvePuzzle(SudokuPuzzle puzzle, bool showLiveView, Action<SudokuPuzzle> showPuzzle)
    {
        var currentCellState = BackTrackingSolveStateEnum.Unknown;
        for (int currentCellIndex = 0; currentCellIndex < puzzle.Cells.Count; currentCellIndex++)
        {
            currentCellState = ModifyCurrentCell(currentCellState, puzzle.Cells[currentCellIndex]);
            currentCellIndex = this.GetNextCellIndex(puzzle.Cells[currentCellIndex], currentCellIndex, currentCellState);

            if (showLiveView)
            {
                showPuzzle(puzzle);

                // Pause a little so we can see.
                Thread.Sleep(50);
            }
        }
    }

    /// <summary>
    /// Solve all of the puzzles in the list using the backtracking sudoku solver.
    /// </summary>
    /// <param name="puzzleList">The list of puzzles to solve.</param>
    /// <param name="showLiveView">Whether or not to show the puzzle as it is being solved.</param>
    /// <param name="showPuzzle">The delegate to show the puzzle in the UI.</param>
    public void SolvePuzzles(List<SudokuPuzzle> puzzleList, bool showLiveView, Action<SudokuPuzzle> showPuzzle)
    {
        if (showLiveView)
        {
            for (int puzzleIndex = 0; puzzleIndex < puzzleList.Count; puzzleIndex++)
            {
                this.SolvePuzzle(puzzleList[puzzleIndex], showLiveView, showPuzzle);
            }
        }
        else
        {
            Parallel.ForEach(puzzleList, puzzle =>
                {
                    try
                    {
                        this.SolvePuzzle(puzzle, false, showPuzzle);
                        Console.WriteLine($"Solved puzzle {puzzle.Id}...");
                    }
                    catch (OutsideOfPuzzleCellBoundsException)
                    {
                        Console.WriteLine($"INVALID puzzle {puzzle.Id}...");
                    }
                });
        }
    }

    private static BackTrackingSolveStateEnum ModifyCurrentCell(BackTrackingSolveStateEnum currentCellState, SudokuPuzzleCell cell)
    {
        if (cell.IsSettable)
        {
            if (cell.IsSet)
            {
                if (cell.CurrentValue == SudokuConstants.MaxValue)
                {
                    // Clear the value and go back to the previous sibling;
                    cell.ClearValue();
                    currentCellState = BackTrackingSolveStateEnum.Overflowed;
                }
                else
                {
                    // Increment if less than 9.
                    cell.CurrentValue++;
                    currentCellState = BackTrackingSolveStateEnum.Incremented;
                }
            }
            else
            {
                // Init to 1 if never set.
                cell.CurrentValue = SudokuConstants.MinValue;
                currentCellState = BackTrackingSolveStateEnum.Initialized;
            }
        }

        return currentCellState;
    }

    /// <summary>
    /// Determines the next cell to modify based on the state and validity of the current cell.
    /// </summary>
    /// <param name="cell">Cell that was just modified.</param>
    /// <param name="currentCellIndex">Position of the cell in the puzzle.</param>
    /// <param name="currentCellState">Modification state of the current cell.</param>
    /// <returns>The index of the next cell to modify. Remember that this gets incremented by 1 at the start of the loop.</returns>
    /// <exception cref="OutsideOfPuzzleCellBoundsException">Thrown if an invalid cell position is selected.</exception>
    private int GetNextCellIndex(SudokuPuzzleCell cell, int currentCellIndex, BackTrackingSolveStateEnum currentCellState)
    {
        var nextCellIndex = currentCellIndex;
        if (!cell.IsSetAndValid())
        {
            if (currentCellState == BackTrackingSolveStateEnum.Overflowed)
            {
                // Go back to the previous cell and modify it.
                nextCellIndex -= 2;

                if (nextCellIndex < -1)
                {
                    throw new OutsideOfPuzzleCellBoundsException();
                }
            }
            else if (currentCellState == BackTrackingSolveStateEnum.Incremented ||
                currentCellState == BackTrackingSolveStateEnum.Initialized)
            {
                // Increment this cell again.
                nextCellIndex--;
            }
        }
        else if (!cell.IsSettable && currentCellState == BackTrackingSolveStateEnum.Overflowed)
        {
            // Go back to the previous cell and increment it.
            nextCellIndex -= 2;
        }

        return nextCellIndex;
    }
}