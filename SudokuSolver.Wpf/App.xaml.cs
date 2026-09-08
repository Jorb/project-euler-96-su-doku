// <copyright file="App.xaml.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf
{
    using System.Windows;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SudokuSolver.Wpf.Models;
    using SudokuSolver.Wpf.Services;
    using SudokuSolver.Wpf.ViewModels;

    /// <summary>
    /// Application entry point. Owns the dependency injection container and the main window.
    /// </summary>
    public partial class App : Application
    {
        private IHost? host;

        /// <inheritdoc/>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = Host.CreateApplicationBuilder();
            ConfigureServices(builder.Services);
            this.host = builder.Build();

            this.host.Services.GetRequiredService<MainWindow>().Show();
        }

        /// <inheritdoc/>
        protected override void OnExit(ExitEventArgs e)
        {
            this.host?.Dispose();
            this.host = null;
            base.OnExit(e);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // The two solvers are registered under their SolverKind so SolverRunner can pick one at run time.
            // Both have implicit parameterless constructors, so they need no factory.
            services.AddKeyedTransient<ISudokuPuzzleSolver, BackTrackingSudokuPuzzleSolver>(SolverKind.BackTracking);
            services.AddKeyedTransient<ISudokuPuzzleSolver, ConstraintSolverWithBacktracking>(SolverKind.ConstraintWithBacktracking);

            services.AddSingleton<SudokuPuzzlePrinter>();

            services.AddSingleton<IPuzzleDefinitionSource, SudokuTextFilePuzzleSource>();
            services.AddSingleton<ISolverRunner, SolverRunner>();
            services.AddSingleton<IComparisonRunner, ComparisonRunner>();
            services.AddSingleton<IPuzzleExportService, PuzzleExportService>();
            services.AddSingleton<IFileDialogService, FileDialogService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }
    }
}
