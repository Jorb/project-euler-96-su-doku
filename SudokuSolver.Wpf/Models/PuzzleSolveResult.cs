// <copyright file="PuzzleSolveResult.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Models
{
    /// <summary>
    /// The outcome of running one solver against one puzzle.
    /// </summary>
    /// <param name="PuzzleId">The grid number of the puzzle that was solved.</param>
    /// <param name="Solver">Which solver produced this result.</param>
    /// <param name="Status">Whether the puzzle was solved, failed or skipped.</param>
    /// <param name="Elapsed">How long the solver took. Meaningless when the live view was enabled.</param>
    /// <param name="ResultLines">The nine grid rows as the solver left them, or an empty list when skipped.</param>
    /// <param name="Error">The failure message, or null when the solver did not throw.</param>
    /// <param name="SolvedPuzzle">The mutated puzzle instance, retained so it can be exported. Null when skipped.</param>
    public sealed record PuzzleSolveResult(
        int PuzzleId,
        SolverKind Solver,
        RunStatus Status,
        TimeSpan Elapsed,
        IReadOnlyList<string> ResultLines,
        string? Error,
        SudokuPuzzle? SolvedPuzzle)
    {
        /// <summary>
        /// Builds a result representing a puzzle that was never attempted because the run was stopped.
        /// </summary>
        /// <param name="puzzleId">The grid number of the puzzle.</param>
        /// <param name="solver">The solver that would have run.</param>
        /// <returns>A skipped result.</returns>
        public static PuzzleSolveResult Skipped(int puzzleId, SolverKind solver) =>
            new PuzzleSolveResult(puzzleId, solver, RunStatus.Skipped, TimeSpan.Zero, Array.Empty<string>(), null, null);

        /// <summary>
        /// Builds a placeholder result announcing that a solver has just started on a puzzle,
        /// so the table can show progress before the solver returns.
        /// </summary>
        /// <param name="puzzleId">The grid number of the puzzle.</param>
        /// <param name="solver">The solver that is starting.</param>
        /// <returns>A running result.</returns>
        public static PuzzleSolveResult Running(int puzzleId, SolverKind solver) =>
            new PuzzleSolveResult(puzzleId, solver, RunStatus.Running, TimeSpan.Zero, Array.Empty<string>(), null, null);
    }
}
