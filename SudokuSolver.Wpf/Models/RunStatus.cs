// <copyright file="RunStatus.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Models
{
    /// <summary>
    /// The state of one puzzle-and-solver pairing within a comparison run.
    /// </summary>
    public enum RunStatus
    {
        /// <summary>
        /// Not attempted yet.
        /// </summary>
        Pending,

        /// <summary>
        /// Currently being solved.
        /// </summary>
        Running,

        /// <summary>
        /// Completed and the puzzle reports itself solved.
        /// </summary>
        Solved,

        /// <summary>
        /// The solver threw, or finished without solving the puzzle.
        /// </summary>
        Failed,

        /// <summary>
        /// The run was stopped before reaching this puzzle.
        /// </summary>
        Skipped,
    }
}
