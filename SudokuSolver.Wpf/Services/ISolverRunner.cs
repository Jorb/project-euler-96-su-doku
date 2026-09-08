// <copyright file="ISolverRunner.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Runs a single solver against a single puzzle and times it.
    /// </summary>
    public interface ISolverRunner
    {
        /// <summary>
        /// Solves one puzzle with one solver.
        /// </summary>
        /// <param name="definition">The puzzle to solve. A fresh puzzle instance is built from it.</param>
        /// <param name="kind">Which solver to use.</param>
        /// <param name="onStep">
        /// When supplied, the solver runs in live view mode and this is invoked with the nine grid rows after every
        /// cell change. Called on the calling thread. Note that live view forces a 50ms sleep per step inside the
        /// library, so any timing measured with it is not comparable.
        /// </param>
        /// <param name="cancellationToken">
        /// Only has an effect in live view mode, where the per step callback gives us a place to bail out from.
        /// A non live solve cannot be interrupted at all, because the library exposes no hook during the loop.
        /// </param>
        /// <returns>The outcome, including elapsed time and the final grid.</returns>
        PuzzleSolveResult Run(
            PuzzleDefinition definition,
            SolverKind kind,
            Action<IReadOnlyList<string>>? onStep,
            CancellationToken cancellationToken);
    }
}
