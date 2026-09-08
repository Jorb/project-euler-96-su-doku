// <copyright file="IPuzzleDefinitionSource.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Loads puzzle definitions from a Project Euler style sudoku definition file.
    /// </summary>
    public interface IPuzzleDefinitionSource
    {
        /// <summary>
        /// Reads every puzzle out of a definition file.
        /// </summary>
        /// <param name="sudokuFilePath">Path to the definition file.</param>
        /// <returns>The puzzles in file order.</returns>
        IReadOnlyList<PuzzleDefinition> LoadAll(string sudokuFilePath);
    }
}
