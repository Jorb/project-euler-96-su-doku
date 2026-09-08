// <copyright file="SudokuTextFilePuzzleSource.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using System.IO;
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Reads puzzle definitions out of the sudoku.txt format: a "Grid NN" header followed by nine rows of nine digits.
    /// <para>
    /// This deliberately does not use <see cref="SudokuPuzzleFactory"/>. That factory discards the source lines and
    /// exposes only puzzles whose Id is internal, so the UI would have no way to label a row or rebuild a puzzle for
    /// a second solver. It also indexes past the end of the array when the file has a trailing blank line.
    /// </para>
    /// </summary>
    public sealed class SudokuTextFilePuzzleSource : IPuzzleDefinitionSource
    {
        private const int RowsPerPuzzle = 9;
        private const int LinesPerPuzzle = RowsPerPuzzle + 1;

        /// <inheritdoc/>
        public IReadOnlyList<PuzzleDefinition> LoadAll(string sudokuFilePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(sudokuFilePath);

            var lines = File.ReadAllLines(sudokuFilePath)
                .Select(line => line.Trim())
                .Where(line => line.Length > 0)
                .ToList();

            var definitions = new List<PuzzleDefinition>();
            for (int index = 0; index + LinesPerPuzzle <= lines.Count; index += LinesPerPuzzle)
            {
                var header = lines[index];
                var rows = lines.GetRange(index + 1, RowsPerPuzzle);

                if (rows.Any(row => row.Length != RowsPerPuzzle || !row.All(char.IsAsciiDigit)))
                {
                    throw new InvalidDataException(
                        $"'{sudokuFilePath}' is malformed near line {index + 1}. Expected nine rows of nine digits under '{header}'.");
                }

                definitions.Add(new PuzzleDefinition(ParseGridId(header, definitions.Count + 1), rows));
            }

            if (definitions.Count == 0)
            {
                throw new InvalidDataException($"'{sudokuFilePath}' contained no puzzles.");
            }

            return definitions;
        }

        /// <summary>
        /// Pulls the grid number out of a "Grid 07" style header, falling back to the puzzle's position in the file.
        /// </summary>
        /// <param name="header">The header line above the nine rows.</param>
        /// <param name="fallbackId">The one based ordinal to use when the header carries no number.</param>
        /// <returns>The grid number.</returns>
        private static int ParseGridId(string header, int fallbackId)
        {
            var digits = new string(header.Where(char.IsAsciiDigit).ToArray());
            return int.TryParse(digits, out var id) ? id : fallbackId;
        }
    }
}
