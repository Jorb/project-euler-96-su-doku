// <copyright file="MainWindow.xaml.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf
{
    using System.Windows;
    using SudokuSolver.Wpf.ViewModels;

    /// <summary>
    /// The application's only window. Everything it does lives in <see cref="MainViewModel"/>.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewModel">The view model, supplied by the container.</param>
        public MainWindow(MainViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
