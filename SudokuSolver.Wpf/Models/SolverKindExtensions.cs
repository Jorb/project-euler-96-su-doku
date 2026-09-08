// <copyright file="SolverKindExtensions.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Models
{
    /// <summary>
    /// Ordering and display helpers for <see cref="SolverKind"/>.
    /// </summary>
    public static class SolverKindExtensions
    {
        /// <summary>
        /// All solver kinds, in the order they should be run and displayed.
        /// </summary>
        public static readonly IReadOnlyList<SolverKind> All = new[]
        {
            SolverKind.BackTracking,
            SolverKind.ConstraintWithBacktracking,
        };

        /// <summary>
        /// Gets a short human readable name for a solver kind.
        /// </summary>
        /// <param name="kind">The solver kind.</param>
        /// <returns>The display name.</returns>
        public static string ToDisplayName(this SolverKind kind) => kind switch
        {
            SolverKind.BackTracking => "Backtracking",
            SolverKind.ConstraintWithBacktracking => "Constraint",
            _ => kind.ToString(),
        };
    }
}
