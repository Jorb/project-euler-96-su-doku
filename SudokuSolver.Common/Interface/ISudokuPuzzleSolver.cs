// <copyright file="ISudokuPuzzleSolver.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Interface for Sudoku puzzle solver.
/// </summary>
public interface ISudokuPuzzleSolver
{
    /// <summary>
    /// Solve a list of Sudoku puzzles.
    /// </summary>
    /// <param name="puzzleList">List of sudoku puzzles to solve.</param>
    /// <param name="showLiveView">Display the puzzles as they are being solved (slower).</param>
    void SolvePuzzles(List<SudokuPuzzle> puzzleList, bool showLiveView, Action<SudokuPuzzle> showPuzzle);

    /// <summary>
    /// Solve a single sudoku puzzle.
    /// </summary>
    /// <param name="puzzle">Sudoku puzzle to solve.</param>
    /// <param name="showLiveView">Display the puzzle as it is being solved (slower).</param>
    void SolvePuzzle(SudokuPuzzle puzzle, bool showLiveView, Action<SudokuPuzzle> showPuzzle);
}