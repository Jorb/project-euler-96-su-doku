// <copyright file="CellViewModel.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;

    /// <summary>
    /// One of the 81 squares in the displayed grid.
    /// <para>
    /// This is the UI's own cell type. The library's SudokuPuzzleCell is internal, so nothing here can bind to it.
    /// </para>
    /// </summary>
    public sealed partial class CellViewModel : ObservableObject
    {
        [ObservableProperty]
        private int? value;

        [ObservableProperty]
        private bool isGiven;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellViewModel"/> class.
        /// </summary>
        /// <param name="row">Zero based row index. 0 is the topmost.</param>
        /// <param name="column">Zero based column index. 0 is the leftmost.</param>
        public CellViewModel(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        /// <summary>
        /// Gets the zero based row index of this cell.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the zero based column index of this cell.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Gets a value indicating whether a thick border should be drawn on the right, marking a 3x3 box edge.
        /// </summary>
        public bool IsBoxEdgeRight => this.Column % 3 == 2 && this.Column != 8;

        /// <summary>
        /// Gets a value indicating whether a thick border should be drawn on the bottom, marking a 3x3 box edge.
        /// </summary>
        public bool IsBoxEdgeBottom => this.Row % 3 == 2 && this.Row != 8;

        /// <summary>
        /// Gets the text to render in the cell. Empty cells show nothing.
        /// </summary>
        public string DisplayText => this.Value?.ToString() ?? string.Empty;

        /// <summary>
        /// Gets a value indicating whether this cell was filled in by a solver rather than given in the puzzle.
        /// </summary>
        public bool IsSolverFilled => !this.IsGiven && this.Value is not null;

        partial void OnValueChanged(int? value)
        {
            this.OnPropertyChanged(nameof(this.DisplayText));
            this.OnPropertyChanged(nameof(this.IsSolverFilled));
        }

        partial void OnIsGivenChanged(bool value)
        {
            this.OnPropertyChanged(nameof(this.IsSolverFilled));
        }
    }
}
