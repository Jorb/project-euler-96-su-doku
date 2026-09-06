// <copyright file="BackTrackingSudokuPuzzleSolver.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;
using SudokuSolver.Console.Helper;

/// <summary>
/// Sudoku puzzle solver using backtracking algorithm.
/// https://en.wikipedia.org/wiki/Sudoku_solving_algorithms#Backtracking .
/// </summary>
internal class BackTrackingSudokuPuzzleSolver : ISudokuPuzzleSolver
{
    public void SolvePuzzle(SudokuPuzzle puzzle, bool showLiveView)
    {
        var currentCellState = CurrentCellStateEnum.Unknown;
        for (int currentCellIndex = 0; currentCellIndex < puzzle.Cells.Count; currentCellIndex++)
        {
            currentCellState = ModifyCurrentCell(currentCellState, puzzle.Cells[currentCellIndex]);
            currentCellIndex = GetNextCellIndex(puzzle.Cells[currentCellIndex], currentCellIndex, currentCellState);

            if (showLiveView)
            {
                PrintPuzzle(puzzle);
            }
        }
    }

    public void SolvePuzzles(List<SudokuPuzzle> puzzleList, bool showLiveView)
    {
        List<Action> solveActions = new List<Action>();
        //for (int puzzleIndex = 0; puzzleIndex < puzzleList.Count; puzzleIndex++)
        //{
        //    solveActions.Add(() => this.SolvePuzzle(puzzleList[puzzleIndex], showLiveView));
        //    //Console.Write($"Solving puzzle {puzzleIndex + 1}...");
        //    //this.SolvePuzzle(puzzleList[puzzleIndex], showLiveView);
        //    //Console.WriteLine("SOLVED");
        //}

        Parallel.ForEach(puzzleList, puzzle =>
        {
            SolvePuzzle(puzzle, showLiveView);
            Console.WriteLine($"Solved puzzle {puzzle.Id}...");
        });
    }

    private static CurrentCellStateEnum ModifyCurrentCell(CurrentCellStateEnum currentCellState, SudokuPuzzleCell cell)
    {
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
        }

        return currentCellState;
    }

    private static void PrintPuzzle(SudokuPuzzle puzzle)
    {
        Console.Clear();
        var puzzleLines = PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle);
        foreach (var puzzleLine in puzzleLines) { Console.WriteLine(puzzleLine); }
        Thread.Sleep(10);
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
}