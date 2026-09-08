// <copyright file="SudokuGridViewModel.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// The 9x9 grid shown beside the results table.
    /// Cells are held in a fixed row major list so the view can lay them out in a UniformGrid.
    /// </summary>
    public sealed partial class SudokuGridViewModel : ObservableObject
    {
        private const int Size = 9;

        [ObservableProperty]
        private string title = "No puzzle selected";

        /// <summary>
        /// Initializes a new instance of the <see cref="SudokuGridViewModel"/> class.
        /// </summary>
        public SudokuGridViewModel()
        {
            var cells = new List<CellViewModel>(Size * Size);
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    cells.Add(new CellViewModel(row, column));
                }
            }

            this.Cells = cells;
        }

        /// <summary>
        /// Gets the 81 cells, ordered left to right then top to bottom.
        /// The collection itself never changes, only the values inside it, so no change notification is needed.
        /// </summary>
        public IReadOnlyList<CellViewModel> Cells { get; }

        /// <summary>
        /// Shows a puzzle in its unsolved state and records which cells are givens.
        /// </summary>
        /// <param name="definition">The puzzle to show, or null to blank the grid.</param>
        public void LoadDefinition(PuzzleDefinition? definition)
        {
            if (definition is null)
            {
                this.Title = "No puzzle selected";
                foreach (var cell in this.Cells)
                {
                    cell.IsGiven = false;
                    cell.Value = null;
                }

                return;
            }

            this.Title = definition.DisplayName;
            foreach (var cell in this.Cells)
            {
                var isGiven = definition.IsGiven(cell.Row, cell.Column);
                cell.IsGiven = isGiven;
                cell.Value = isGiven ? definition.Lines[cell.Row][cell.Column] - '0' : null;
            }
        }

        /// <summary>
        /// Updates the cell values from nine rows of nine digits, leaving the givens flags alone.
        /// Used both for a finished result and for each step of a live run.
        /// </summary>
        /// <param name="lines">The nine grid rows, '0' meaning empty.</param>
        public void ApplyLines(IReadOnlyList<string> lines)
        {
            if (lines is null || lines.Count != Size)
            {
                return;
            }

            foreach (var cell in this.Cells)
            {
                var character = lines[cell.Row][cell.Column];
                cell.Value = character == '0' ? null : character - '0';
            }
        }
    }
}
