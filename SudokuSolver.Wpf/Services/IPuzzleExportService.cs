// <copyright file="IPuzzleExportService.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Writes solved puzzles back out in the definition file format.
    /// </summary>
    public interface IPuzzleExportService
    {
        /// <summary>
        /// Writes the solved grids of one solver's results to a file.
        /// </summary>
        /// <param name="results">The results to export. Anything without a puzzle attached is ignored.</param>
        /// <param name="outputPath">Where to write the file.</param>
        /// <returns>The number of puzzles written.</returns>
        int Export(IEnumerable<PuzzleSolveResult> results, string outputPath);

        /// <summary>
        /// Builds the default file name to offer for a solver's export.
        /// </summary>
        /// <param name="solver">The solver whose results are being exported.</param>
        /// <returns>A suggested file name.</returns>
        string BuildSuggestedFileName(SolverKind solver);
    }
}
