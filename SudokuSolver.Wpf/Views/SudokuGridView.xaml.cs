// <copyright file="SudokuGridView.xaml.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Views
{
    using System.Windows.Controls;

    /// <summary>
    /// Renders a 9x9 sudoku grid bound to a SudokuGridViewModel.
    /// </summary>
    public partial class SudokuGridView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SudokuGridView"/> class.
        /// </summary>
        public SudokuGridView()
        {
            this.InitializeComponent();
        }
    }
}
