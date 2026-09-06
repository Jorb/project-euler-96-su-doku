// <copyright file="SudokuPuzzleFactory.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Helper;

/// <summary>
/// Builds puzzle in-memory objects by deserialzing a puzzle file.
/// </summary>
public class SudokuPuzzleFactory
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuPuzzleFactory"/> class.
    /// </summary>
    public SudokuPuzzleFactory()
    {
    }

    /// <summary>
    /// Build list of puzzle objects by deserializing definition file.
    /// </summary>
    /// <param name="sudokuFilePath">Sudoku puzzle definition file.</param>
    /// <returns>A list of Sudoku puzzle objects.</returns>
    public List<SudokuPuzzle> BuildAllPuzzlesFromFile(string sudokuFilePath)
    {
        List<SudokuPuzzle> sudokuPuzzleList = new List<SudokuPuzzle>();

        var rawsudokuLines = File.ReadAllLines(sudokuFilePath);

        // Parse out each puzzle (could do this with fancy regex, but I don't wanna right now.)
        for (int i = 0; i < rawsudokuLines.Length; i += 10)
        {
            List<string> puzzleLines = new List<string>();
            for (int j = i + 1; j < i + 10; j++)
            {
                puzzleLines.Add(rawsudokuLines[j]);
            }

            var puzzleIndex = i / 10;
            sudokuPuzzleList.Add(new SudokuPuzzle(puzzleIndex + 1, puzzleLines));
        }

        return sudokuPuzzleList;
    }
}