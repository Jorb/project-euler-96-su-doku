// <copyright file="SolverKind.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Models
{
    /// <summary>
    /// Identifies which of the solver implementations to run.
    /// Doubles as the service key for the keyed <see cref="ISudokuPuzzleSolver"/> registrations.
    /// </summary>
    public enum SolverKind
    {
        /// <summary>
        /// The naive backtracking solver, <see cref="BackTrackingSudokuPuzzleSolver"/>.
        /// </summary>
        BackTracking,

        /// <summary>
        /// The constraint solver with backtracking, <see cref="ConstraintSolverWithBacktracking"/>.
        /// </summary>
        ConstraintWithBacktracking,
    }
}
