// <copyright file="PuzzleComparisonRowViewModel.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// One row of the comparison table: a single puzzle with a column per solver.
    /// </summary>
    public sealed partial class PuzzleComparisonRowViewModel : ObservableObject
    {
        [ObservableProperty]
        private RunStatus backTrackingStatus = RunStatus.Pending;

        [ObservableProperty]
        private RunStatus constraintStatus = RunStatus.Pending;

        [ObservableProperty]
        private double? backTrackingMilliseconds;

        [ObservableProperty]
        private double? constraintMilliseconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="PuzzleComparisonRowViewModel"/> class.
        /// </summary>
        /// <param name="definition">The puzzle this row represents.</param>
        public PuzzleComparisonRowViewModel(PuzzleDefinition definition)
        {
            this.Definition = definition;
        }

        /// <summary>
        /// Gets the puzzle this row represents.
        /// </summary>
        public PuzzleDefinition Definition { get; }

        /// <summary>
        /// Gets the grid number of the puzzle.
        /// </summary>
        public int PuzzleId => this.Definition.Id;

        /// <summary>
        /// Gets the failure message from either solver, or null when neither failed.
        /// </summary>
        public string? Error { get; private set; }

        /// <summary>
        /// Gets the solved grid rows of the last solver to finish this puzzle, or null.
        /// </summary>
        public IReadOnlyList<string>? LatestResultLines { get; private set; }

        /// <summary>
        /// Gets the name of the faster solver, or a placeholder when both have not finished.
        /// </summary>
        public string FasterSolver
        {
            get
            {
                if (this.BackTrackingMilliseconds is not double backTracking ||
                    this.ConstraintMilliseconds is not double constraint)
                {
                    return string.Empty;
                }

                return constraint < backTracking
                    ? SolverKind.ConstraintWithBacktracking.ToDisplayName()
                    : SolverKind.BackTracking.ToDisplayName();
            }
        }

        /// <summary>
        /// Gets how many times faster the winning solver was, or null when both have not finished.
        /// </summary>
        public double? Speedup
        {
            get
            {
                if (this.BackTrackingMilliseconds is not double backTracking ||
                    this.ConstraintMilliseconds is not double constraint)
                {
                    return null;
                }

                var slower = Math.Max(backTracking, constraint);
                var faster = Math.Min(backTracking, constraint);
                return faster <= 0 ? null : slower / faster;
            }
        }

        /// <summary>
        /// Folds a result from the runner into this row.
        /// </summary>
        /// <param name="result">The result to apply.</param>
        public void Apply(PuzzleSolveResult result)
        {
            if (result.Solver == SolverKind.BackTracking)
            {
                this.BackTrackingStatus = result.Status;
                this.BackTrackingMilliseconds = result.Status is RunStatus.Solved or RunStatus.Failed
                    ? result.Elapsed.TotalMilliseconds
                    : null;
            }
            else
            {
                this.ConstraintStatus = result.Status;
                this.ConstraintMilliseconds = result.Status is RunStatus.Solved or RunStatus.Failed
                    ? result.Elapsed.TotalMilliseconds
                    : null;
            }

            if (result.Error is not null)
            {
                this.Error = result.Error;
            }

            if (result.ResultLines.Count == 9)
            {
                this.LatestResultLines = result.ResultLines;
            }

            this.RaiseDerivedChanged();
        }

        /// <summary>
        /// Returns the row to its pre-run state.
        /// </summary>
        public void Reset()
        {
            this.BackTrackingStatus = RunStatus.Pending;
            this.ConstraintStatus = RunStatus.Pending;
            this.BackTrackingMilliseconds = null;
            this.ConstraintMilliseconds = null;
            this.Error = null;
            this.LatestResultLines = null;
            this.RaiseDerivedChanged();
        }

        private void RaiseDerivedChanged()
        {
            this.OnPropertyChanged(nameof(this.FasterSolver));
            this.OnPropertyChanged(nameof(this.Speedup));
            this.OnPropertyChanged(nameof(this.Error));
        }
    }
}
