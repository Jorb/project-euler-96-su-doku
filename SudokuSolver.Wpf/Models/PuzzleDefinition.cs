// <copyright file="PuzzleDefinition.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Models
{
    /// <summary>
    /// The immutable definition of a single unsolved puzzle as it was read from the definition file.
    /// <para>
    /// This exists because <see cref="SudokuPuzzle"/> exposes neither its Id nor its cells publicly,
    /// and the solvers mutate the puzzle in place. Keeping the original nine lines lets us build a
    /// fresh <see cref="SudokuPuzzle"/> for every solver run and still know which cells were givens.
    /// </para>
    /// </summary>
    /// <param name="Id">The grid number from the definition file. 1 based.</param>
    /// <param name="Lines">The nine rows of the puzzle. Each is nine characters, '0' meaning empty.</param>
    public sealed record PuzzleDefinition(int Id, IReadOnlyList<string> Lines)
    {
        /// <summary>
        /// Gets the display name of the puzzle, matching the definition file header.
        /// </summary>
        public string DisplayName => $"Grid {this.Id:D2}";

        /// <summary>
        /// Determines whether the cell at the supplied coordinates was a clue in the original puzzle.
        /// </summary>
        /// <param name="row">Zero based row index. 0 is the topmost.</param>
        /// <param name="column">Zero based column index. 0 is the leftmost.</param>
        /// <returns>True when the cell had a value before any solver ran.</returns>
        public bool IsGiven(int row, int column) => this.Lines[row][column] != '0';

        /// <summary>
        /// Builds a brand new solver-ready puzzle from this definition.
        /// Every call returns an independent instance, which matters because the solvers mutate in place.
        /// </summary>
        /// <returns>A fresh unsolved <see cref="SudokuPuzzle"/>.</returns>
        public SudokuPuzzle CreatePuzzle() => new SudokuPuzzle(this.Id, this.Lines.ToList());
    }
}
