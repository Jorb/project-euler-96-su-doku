// <copyright file="PuzzleExportService.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Exports solved puzzles using the library's own printer, so the output matches what the console app produces.
    /// </summary>
    public sealed class PuzzleExportService : IPuzzleExportService
    {
        private readonly SudokuPuzzlePrinter printer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PuzzleExportService"/> class.
        /// </summary>
        /// <param name="printer">The library printer that serializes puzzles back to the file format.</param>
        public PuzzleExportService(SudokuPuzzlePrinter printer)
        {
            this.printer = printer;
        }

        /// <inheritdoc/>
        public int Export(IEnumerable<PuzzleSolveResult> results, string outputPath)
        {
            ArgumentNullException.ThrowIfNull(results);
            ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);

            var puzzles = results
                .OrderBy(result => result.PuzzleId)
                .Select(result => result.SolvedPuzzle)
                .OfType<SudokuPuzzle>()
                .ToList();

            this.printer.PrintPuzzlesToFile(puzzles, outputPath);
            return puzzles.Count;
        }

        /// <inheritdoc/>
        public string BuildSuggestedFileName(SolverKind solver) =>
            $"sudokuSolved_{solver}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
    }
}
