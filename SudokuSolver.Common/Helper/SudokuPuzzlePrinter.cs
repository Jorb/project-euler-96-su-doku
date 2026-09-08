// <copyright file="SudokuPuzzlePrinter.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Helper;

/// <summary>
/// Print in-memory sudoku puzzles to serialized file.
/// </summary>
public class SudokuPuzzlePrinter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuPuzzlePrinter"/> class.
    /// </summary>
    public SudokuPuzzlePrinter()
    {
    }

    /// <summary>
    /// Print a list of puzzles to a file in the same format as the definition file.
    /// </summary>
    /// <param name="puzzleList">List of puzzles to print.</param>
    /// <param name="outputPath">Output path of the puzzle file.</param>
    public void PrintPuzzlesToFile(List<SudokuPuzzle> puzzleList, string outputPath)
    {
        var fileLines = new List<string>();
        foreach (var puzzle in puzzleList)
        {
            // Convert the puzzle back into the file format using the rows objects
            fileLines.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle));
        }

        File.WriteAllLines(outputPath, fileLines);
    }
}