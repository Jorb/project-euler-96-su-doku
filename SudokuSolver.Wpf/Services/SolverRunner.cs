// <copyright file="SolverRunner.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using System.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using SudokuSolver.Common.Helper;
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// The one place in this project that talks to the solver library.
    /// Converts between the UI's nine-strings representation and <see cref="SudokuPuzzle"/>.
    /// </summary>
    public sealed class SolverRunner : ISolverRunner
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolverRunner"/> class.
        /// </summary>
        /// <param name="serviceProvider">Used to resolve the keyed <see cref="ISudokuPuzzleSolver"/> registrations.</param>
        public SolverRunner(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Reads the current state of a puzzle as nine rows of nine digits.
        /// The helper returns a "Grid NN" header as its first line, which we drop.
        /// </summary>
        /// <param name="puzzle">The puzzle to read.</param>
        /// <returns>The nine grid rows.</returns>
        public static IReadOnlyList<string> ReadGridLines(SudokuPuzzle puzzle)
        {
            var fileLines = PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle);
            return fileLines.Skip(1).ToList();
        }

        /// <inheritdoc/>
        public PuzzleSolveResult Run(
            PuzzleDefinition definition,
            SolverKind kind,
            Action<IReadOnlyList<string>>? onStep,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(definition);

            var puzzle = definition.CreatePuzzle();
            var solver = this.serviceProvider.GetRequiredKeyedService<ISudokuPuzzleSolver>(kind);
            var showLiveView = onStep is not null;

            // The callback fires on this thread, the same one mutating the puzzle, so reading it here is safe.
            // Only the resulting immutable strings are handed onwards.
            //
            // Throwing from here is also our only way to stop a solve in progress: the library's loop has no
            // cancellation hook, but it does call us every step, and it has no try/finally that would swallow this.
            Action<SudokuPuzzle> showPuzzle = solving =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                onStep?.Invoke(ReadGridLines(solving));
            };

            var stopwatch = Stopwatch.StartNew();
            string? error = null;
            var cancelled = false;
            try
            {
                solver.SolvePuzzle(puzzle, showLiveView, showPuzzle);
            }
            catch (OperationCanceledException)
            {
                cancelled = true;
            }
            catch (Exception exception)
            {
                // The library signals an unsolvable puzzle by throwing. Only OutsideOfPuzzleCellBoundsException is
                // public; the constraint solver can also surface internal exception types and IndexOutOfRangeException,
                // so there is nothing narrower to catch here.
                error = $"{exception.GetType().Name}: {exception.Message}";
            }

            stopwatch.Stop();

            var gridLines = ReadGridLines(puzzle);

            if (cancelled)
            {
                return new PuzzleSolveResult(
                    definition.Id, kind, RunStatus.Skipped, TimeSpan.Zero, gridLines, "Stopped before finishing.", null);
            }

            var solved = error is null && puzzle.IsSolved;
            if (error is null && !solved)
            {
                error = "The solver finished without solving the puzzle.";
            }

            return new PuzzleSolveResult(
                definition.Id,
                kind,
                solved ? RunStatus.Solved : RunStatus.Failed,
                stopwatch.Elapsed,
                gridLines,
                error,
                puzzle);
        }
    }
}
