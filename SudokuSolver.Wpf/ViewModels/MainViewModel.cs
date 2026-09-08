// <copyright file="MainViewModel.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.ViewModels
{
    using System.Collections.ObjectModel;
    using System.IO;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using SudokuSolver.Wpf.Models;
    using SudokuSolver.Wpf.Services;

    /// <summary>
    /// Drives the main window: loads the bundled puzzles, runs both solvers head to head, and reports the timings.
    /// </summary>
    public sealed partial class MainViewModel : ObservableObject
    {
        private const string PuzzleFileName = "sudoku.txt";

        private readonly IPuzzleDefinitionSource definitionSource;
        private readonly IComparisonRunner comparisonRunner;
        private readonly ISolverRunner solverRunner;
        private readonly IPuzzleExportService exportService;
        private readonly IFileDialogService fileDialogService;
        private readonly Dictionary<(int PuzzleId, SolverKind Solver), PuzzleSolveResult> results =
            new Dictionary<(int PuzzleId, SolverKind Solver), PuzzleSolveResult>();

        private IReadOnlyList<PuzzleDefinition> definitions = Array.Empty<PuzzleDefinition>();
        private CancellationTokenSource? cancellationTokenSource;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunCommand))]
        private PuzzleComparisonRowViewModel? selectedRow;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RunButtonLabel))]
        [NotifyCanExecuteChangedFor(nameof(RunCommand))]
        private bool isLiveViewEnabled;

        [ObservableProperty]
        private SolverKind liveViewSolver = SolverKind.ConstraintWithBacktracking;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RunCommand))]
        [NotifyCanExecuteChangedFor(nameof(StopCommand))]
        [NotifyCanExecuteChangedFor(nameof(ResetCommand))]
        [NotifyCanExecuteChangedFor(nameof(ExportCommand))]
        private bool isRunning;

        [ObservableProperty]
        private string statusMessage = "Ready.";

        [ObservableProperty]
        private bool lastRunUsedLiveView;

        [ObservableProperty]
        private double backTrackingTotalMilliseconds;

        [ObservableProperty]
        private double constraintTotalMilliseconds;

        [ObservableProperty]
        private int solvedCount;

        [ObservableProperty]
        private int failedCount;

        [ObservableProperty]
        private double? overallSpeedup;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="definitionSource">Loads the bundled puzzle definitions.</param>
        /// <param name="comparisonRunner">Runs every solver against every puzzle.</param>
        /// <param name="solverRunner">Runs a single animated solve.</param>
        /// <param name="exportService">Writes solved puzzles to disk.</param>
        /// <param name="fileDialogService">Asks the user where to save.</param>
        public MainViewModel(
            IPuzzleDefinitionSource definitionSource,
            IComparisonRunner comparisonRunner,
            ISolverRunner solverRunner,
            IPuzzleExportService exportService,
            IFileDialogService fileDialogService)
        {
            this.definitionSource = definitionSource;
            this.comparisonRunner = comparisonRunner;
            this.solverRunner = solverRunner;
            this.exportService = exportService;
            this.fileDialogService = fileDialogService;

            this.LoadPuzzles();
        }

        /// <summary>
        /// Gets the comparison table rows, one per puzzle.
        /// </summary>
        public ObservableCollection<PuzzleComparisonRowViewModel> Rows { get; } =
            new ObservableCollection<PuzzleComparisonRowViewModel>();

        /// <summary>
        /// Gets the 9x9 grid shown for the selected puzzle.
        /// </summary>
        public SudokuGridViewModel Grid { get; } = new SudokuGridViewModel();

        /// <summary>
        /// Gets the solver kinds offered in the live view picker.
        /// </summary>
        public IReadOnlyList<SolverKind> SolverKinds => SolverKindExtensions.All;

        /// <summary>
        /// Gets the label for the primary action button, which changes with the live view toggle.
        /// </summary>
        public string RunButtonLabel => this.IsLiveViewEnabled ? "Animate selected" : "Run comparison";

        /// <summary>
        /// Gets a warning to show under the totals when the last run was animated and its timings mean nothing.
        /// </summary>
        public string TotalsCaveat => this.LastRunUsedLiveView
            ? "Timings are not comparable: live view adds a fixed 50ms pause per step."
            : string.Empty;

        /// <summary>
        /// Runs the comparison over every puzzle, or animates the selected one when live view is on.
        /// </summary>
        /// <returns>A task that completes when the run finishes or is stopped.</returns>
        [RelayCommand(CanExecute = nameof(CanRun))]
        private async Task RunAsync()
        {
            this.cancellationTokenSource?.Dispose();
            this.cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = this.cancellationTokenSource.Token;

            this.IsRunning = true;
            this.LastRunUsedLiveView = this.IsLiveViewEnabled;

            try
            {
                if (this.IsLiveViewEnabled)
                {
                    await this.RunLiveAsync(cancellationToken);
                }
                else
                {
                    await this.RunComparisonAsync(cancellationToken);
                }
            }
            catch (Exception exception)
            {
                this.StatusMessage = $"Run failed: {exception.Message}";
            }
            finally
            {
                this.IsRunning = false;
            }
        }

        /// <summary>
        /// Requests that the run stop.
        /// </summary>
        [RelayCommand(CanExecute = nameof(IsRunning))]
        private void Stop()
        {
            this.cancellationTokenSource?.Cancel();
            this.StatusMessage = this.LastRunUsedLiveView
                ? "Stopping..."
                : "Stopping after the puzzle currently being solved finishes...";
        }

        /// <summary>
        /// Clears all results and reloads the puzzles from disk.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanEditState))]
        private void Reset()
        {
            this.LoadPuzzles();
            this.StatusMessage = "Reset.";
        }

        /// <summary>
        /// Exports one solver's solved grids to a file the user chooses.
        /// </summary>
        /// <param name="solver">Which solver's results to export.</param>
        [RelayCommand(CanExecute = nameof(CanExport))]
        private void Export(SolverKind solver)
        {
            var solverResults = this.results.Values
                .Where(result => result.Solver == solver && result.SolvedPuzzle is not null)
                .ToList();

            if (solverResults.Count == 0)
            {
                this.StatusMessage = $"Nothing to export for {solver.ToDisplayName()}. Run it first.";
                return;
            }

            var path = this.fileDialogService.PromptForSavePath(this.exportService.BuildSuggestedFileName(solver));
            if (path is null)
            {
                return;
            }

            try
            {
                var written = this.exportService.Export(solverResults, path);
                this.StatusMessage = $"Exported {written} puzzle(s) from {solver.ToDisplayName()} to {path}.";
            }
            catch (Exception exception)
            {
                this.StatusMessage = $"Export failed: {exception.Message}";
            }
        }

        private bool CanRun() => !this.IsRunning && (!this.IsLiveViewEnabled || this.SelectedRow is not null);

        private bool CanEditState() => !this.IsRunning;

        private bool CanExport(SolverKind solver) =>
            !this.IsRunning && this.results.Values.Any(result => result.Solver == solver && result.SolvedPuzzle is not null);

        /// <summary>
        /// Runs both solvers over every puzzle on a background thread.
        /// The IProgress is built here, on the UI thread, so its callbacks marshal back automatically.
        /// </summary>
        /// <param name="cancellationToken">Stops the run between puzzles.</param>
        /// <returns>A task that completes when the run finishes.</returns>
        private async Task RunComparisonAsync(CancellationToken cancellationToken)
        {
            this.ClearResults();
            this.StatusMessage = $"Running {this.definitions.Count} puzzles against {this.SolverKinds.Count} solvers...";

            var progress = new Progress<PuzzleSolveResult>(this.ApplyResult);
            var toRun = this.definitions;

            await Task.Run(() => this.comparisonRunner.RunComparison(toRun, progress, cancellationToken), CancellationToken.None);

            this.StatusMessage = cancellationToken.IsCancellationRequested
                ? $"Stopped. {this.SolvedCount} solved, {this.FailedCount} failed."
                : $"Done. {this.SolvedCount} solved, {this.FailedCount} failed.";
        }

        /// <summary>
        /// Animates a single puzzle so the solver's behaviour is visible.
        /// Only one puzzle is animated because the library sleeps 50ms per step.
        /// </summary>
        /// <param name="cancellationToken">Stops the animation at the next step.</param>
        /// <returns>A task that completes when the solve finishes or is stopped.</returns>
        private async Task RunLiveAsync(CancellationToken cancellationToken)
        {
            var row = this.SelectedRow;
            if (row is null)
            {
                return;
            }

            var solver = this.LiveViewSolver;
            this.Grid.LoadDefinition(row.Definition);
            this.StatusMessage = $"Animating {row.Definition.DisplayName} with the {solver.ToDisplayName()} solver...";

            var stepProgress = new Progress<IReadOnlyList<string>>(this.Grid.ApplyLines);
            var resultProgress = new Progress<PuzzleSolveResult>(this.ApplyResult);

            var definition = row.Definition;
            await Task.Run(
                () =>
                {
                    ((IProgress<PuzzleSolveResult>)resultProgress).Report(PuzzleSolveResult.Running(definition.Id, solver));
                    var result = this.solverRunner.Run(
                        definition,
                        solver,
                        lines => ((IProgress<IReadOnlyList<string>>)stepProgress).Report(lines),
                        cancellationToken);
                    ((IProgress<PuzzleSolveResult>)resultProgress).Report(result);
                },
                CancellationToken.None);

            this.StatusMessage = cancellationToken.IsCancellationRequested
                ? $"Stopped animating {definition.DisplayName}."
                : $"Finished animating {definition.DisplayName}.";
        }

        /// <summary>
        /// Folds one result into the matching row and refreshes the totals. Always runs on the UI thread.
        /// </summary>
        /// <param name="result">The result to apply.</param>
        private void ApplyResult(PuzzleSolveResult result)
        {
            var row = this.Rows.FirstOrDefault(candidate => candidate.PuzzleId == result.PuzzleId);
            row?.Apply(result);

            if (result.Status is RunStatus.Solved or RunStatus.Failed)
            {
                this.results[(result.PuzzleId, result.Solver)] = result;
            }

            // Show the finished grid for whichever puzzle the user is looking at, without stealing their selection.
            if (row is not null && ReferenceEquals(row, this.SelectedRow) && result.ResultLines.Count == 9)
            {
                this.Grid.ApplyLines(result.ResultLines);
            }

            this.RecalculateTotals();
            this.ExportCommand.NotifyCanExecuteChanged();
        }

        private void RecalculateTotals()
        {
            var completed = this.results.Values.ToList();

            this.BackTrackingTotalMilliseconds = completed
                .Where(result => result.Solver == SolverKind.BackTracking)
                .Sum(result => result.Elapsed.TotalMilliseconds);

            this.ConstraintTotalMilliseconds = completed
                .Where(result => result.Solver == SolverKind.ConstraintWithBacktracking)
                .Sum(result => result.Elapsed.TotalMilliseconds);

            this.SolvedCount = completed.Count(result => result.Status == RunStatus.Solved);
            this.FailedCount = completed.Count(result => result.Status == RunStatus.Failed);

            var slower = Math.Max(this.BackTrackingTotalMilliseconds, this.ConstraintTotalMilliseconds);
            var faster = Math.Min(this.BackTrackingTotalMilliseconds, this.ConstraintTotalMilliseconds);
            this.OverallSpeedup = faster > 0 ? slower / faster : null;
        }

        private void ClearResults()
        {
            this.results.Clear();
            foreach (var row in this.Rows)
            {
                row.Reset();
            }

            this.RecalculateTotals();
            this.LastRunUsedLiveView = false;
            this.ExportCommand.NotifyCanExecuteChanged();
        }

        private void LoadPuzzles()
        {
            var path = Path.Combine(AppContext.BaseDirectory, PuzzleFileName);

            try
            {
                this.definitions = this.definitionSource.LoadAll(path);
            }
            catch (Exception exception)
            {
                this.definitions = Array.Empty<PuzzleDefinition>();
                this.StatusMessage = $"Could not load '{path}': {exception.Message}";
            }

            this.results.Clear();
            this.Rows.Clear();
            foreach (var definition in this.definitions)
            {
                this.Rows.Add(new PuzzleComparisonRowViewModel(definition));
            }

            this.SelectedRow = this.Rows.FirstOrDefault();
            this.RecalculateTotals();
            this.LastRunUsedLiveView = false;

            if (this.definitions.Count > 0)
            {
                this.StatusMessage = $"Loaded {this.definitions.Count} puzzles from {PuzzleFileName}.";
            }
        }

        partial void OnSelectedRowChanged(PuzzleComparisonRowViewModel? value)
        {
            this.Grid.LoadDefinition(value?.Definition);
            if (value?.LatestResultLines is IReadOnlyList<string> lines)
            {
                this.Grid.ApplyLines(lines);
            }
        }

        partial void OnLastRunUsedLiveViewChanged(bool value)
        {
            this.OnPropertyChanged(nameof(this.TotalsCaveat));
        }
    }
}
