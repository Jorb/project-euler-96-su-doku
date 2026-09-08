// <copyright file="ComparisonRunner.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Drives the head to head comparison run.
    /// <para>
    /// This deliberately does not call <see cref="ISudokuPuzzleSolver.SolvePuzzles"/>. That method's batch path uses
    /// Parallel.ForEach and writes its progress to the console, which gives us neither per puzzle timings nor anything
    /// visible in a window. Running one puzzle at a time on one thread is also the only way the two solvers' numbers
    /// can be fairly compared.
    /// </para>
    /// </summary>
    public sealed class ComparisonRunner : IComparisonRunner
    {
        private readonly ISolverRunner solverRunner;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonRunner"/> class.
        /// </summary>
        /// <param name="solverRunner">Runs and times an individual puzzle.</param>
        public ComparisonRunner(ISolverRunner solverRunner)
        {
            this.solverRunner = solverRunner;
        }

        /// <inheritdoc/>
        public void RunComparison(
            IReadOnlyList<PuzzleDefinition> definitions,
            IProgress<PuzzleSolveResult> progress,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(definitions);
            ArgumentNullException.ThrowIfNull(progress);

            var stopped = false;
            foreach (var definition in definitions)
            {
                foreach (var kind in SolverKindExtensions.All)
                {
                    if (stopped || cancellationToken.IsCancellationRequested)
                    {
                        stopped = true;
                        progress.Report(PuzzleSolveResult.Skipped(definition.Id, kind));
                        continue;
                    }

                    progress.Report(PuzzleSolveResult.Running(definition.Id, kind));
                    progress.Report(this.solverRunner.Run(definition, kind, onStep: null, cancellationToken));
                }
            }
        }
    }
}
