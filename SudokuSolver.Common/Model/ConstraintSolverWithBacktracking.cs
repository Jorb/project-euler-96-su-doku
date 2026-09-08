// <copyright file="ConstraintSolverWithBacktracking.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Const;
using SudokuSolver.Common.Helper;

/// <summary>
/// Joe's attempt at improving the backtracking solver by constraining the values in real time.
/// Does a first pass to find all of the cells with single possibilities first.
/// </summary>
public class ConstraintSolverWithBacktracking : ISudokuPuzzleSolver
{
    /// <summary>
    /// Solve the sudoku puzzle using the constraint solver with backtracking.
    /// </summary>
    /// <param name="puzzle">The sudoku puzzle to solve.</param>
    /// <param name="showLiveView">Whether or not to show the live puzzle being solved.</param>
    /// <param name="showPuzzle">The delegate to show the puzzle in the UI.</param>
    public void SolvePuzzle(SudokuPuzzle puzzle, bool showLiveView, Action<SudokuPuzzle> showPuzzle)
    {
        List<SudokuPuzzleCell> settableCells = puzzle.Cells.Where(cell => cell.IsSettable).ToList();

        // First pass set all single possibilities.
        foreach (var cell in settableCells)
        {
            if (cell.IsLocked)
            {
                continue;
            }

            List<int> possibleValues = cell.GetPossibleValues();
            if (possibleValues.Count == 1)
            {
                cell.CurrentValue = possibleValues[0];
                cell.Lock();
                if (showLiveView)
                {
                    showPuzzle(puzzle);
                }
            }
        }

        settableCells = puzzle.Cells.Where(cell => cell.IsSettable && !cell.IsLocked).ToList();
        var currentCellState = BackTrackingSolveStateEnum.Unknown;
        for (int currentCellIndex = 0; currentCellIndex < settableCells.Count(); currentCellIndex++)
        {
            var cell = settableCells[currentCellIndex];
            currentCellState = ModifyCurrentCell(currentCellState, cell);
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
    /// Solve the sudoku puzzles using the constraint solver with backtracking.
    /// </summary>
    /// <param name="puzzleList">List of puzzles to solve.</param>
    /// <param name="showLiveView">Whether or not to show the live puzzle being solved.</param>
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
                    this.SolvePuzzle(puzzle, false, showPuzzle);
                    Console.WriteLine($"Solved puzzle {puzzle.Id}...");
                });
        }
    }

    private static BackTrackingSolveStateEnum ModifyCurrentCell(BackTrackingSolveStateEnum currentCellState, SudokuPuzzleCell cell)
    {
        // Get the value of the current cell.
        var currentValue = cell.CurrentValue;

        // Clear the value so we can get all possible options
        cell.ClearValue();

        // Get all the possible options.
        var possibleValues = cell.GetPossibleValues();

        if (possibleValues.Count == 0)
        {
            // If there are no possible values then we have overflowed.
            currentCellState = BackTrackingSolveStateEnum.Overflowed;
        }
        else if (cell.CurrentValue is null && currentCellState != BackTrackingSolveStateEnum.Overflowed)
        {
            // If the currentValue is empty (null), set it to the first possible value.
            cell.CurrentValue = possibleValues[0];
            currentCellState = BackTrackingSolveStateEnum.Initialized;
        }
        else if (currentValue == possibleValues.Last())
        {
            // If we have already assigned the last possible value, we need to clear the current value and increment the previous.
            currentCellState = BackTrackingSolveStateEnum.Overflowed;
        }
        else
        {
            if (currentValue is null)
            {
                throw new ConstraintSolverEncounteredUnexpectedNullCell();
            }

            // If there is a possible value larger than the current value, assign it.
            var nextIndex = possibleValues.IndexOf((int)currentValue) + 1;
            cell.CurrentValue = possibleValues[nextIndex];
            currentCellState = BackTrackingSolveStateEnum.Incremented;
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
        else if (currentCellState == BackTrackingSolveStateEnum.Overflowed)
        {
            // Go back to the previous cell and increment it.
            nextCellIndex -= 2;
        }

        return nextCellIndex;
    }
}