// <copyright file="IComparisonRunner.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Runs every solver against every puzzle so their timings can be compared.
    /// </summary>
    public interface IComparisonRunner
    {
        /// <summary>
        /// Runs all solvers against all puzzles, sequentially and single threaded so the timings stay honest.
        /// </summary>
        /// <param name="definitions">The puzzles to run.</param>
        /// <param name="progress">Receives a Running result as each solve starts and the real result as it finishes.</param>
        /// <param name="cancellationToken">
        /// Checked between puzzles only. The library offers no way to interrupt a solve in flight,
        /// so the puzzle currently being solved always runs to completion.
        /// </param>
        void RunComparison(
            IReadOnlyList<PuzzleDefinition> definitions,
            IProgress<PuzzleSolveResult> progress,
            CancellationToken cancellationToken);
    }
}
