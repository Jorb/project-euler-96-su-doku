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

        this.ValidatePuzzlesAgainstFile(sudokuPuzzleList, rawsudokuLines);
        return sudokuPuzzleList;
    }

    /// <summary>
    /// Build the sudoku input file using each of the objects to prove it was built properly.
    /// </summary>
    /// <param name="puzzleList">Collection of puzzle objects to print.</param>
    /// <param name="rawsudokuLines">The raw lines from the input file.</param>
    /// <exception cref="InMemorySudokuPuzzlesDontMatchFileException">Thrown if the puzzle objects weren't built properly.</exception>
    private void ValidatePuzzlesAgainstFile(List<SudokuPuzzle> puzzleList, string[] rawsudokuLines)
    {
        List<string> fileLinesFromRows = new List<string>();
        List<string> fileLinesFromColumns = new List<string>();
        List<string> fileLinesFromBoxes = new List<string>();

        var puzzleCount = 1;
        foreach (var puzzle in puzzleList)
        {
            // Convert the puzzle back into the file format using the rows objects
            fileLinesFromRows.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle));

            // Convert the puzzle back into the file format using the columns objects
            fileLinesFromColumns.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleColumns(puzzle));

            // Convert the puzzle back into the file format using the box objects
            fileLinesFromBoxes.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleBoxes(puzzle));

            puzzleCount++;
        }

        for (int i = 0; i < rawsudokuLines.Length; i++)
        {
            // Validate row construction.
            if (!fileLinesFromRows[i].Equals(rawsudokuLines[i]))
            {
                throw new InMemorySudokuPuzzlesDontMatchFileException();
            }

            // Validate Column construction.
            if (!fileLinesFromColumns[i].Equals(rawsudokuLines[i]))
            {
                throw new InMemorySudokuPuzzlesDontMatchFileException();
            }

            // Validate Box construction.
            if (!fileLinesFromBoxes[i].Equals(rawsudokuLines[i]))
            {
                throw new InMemorySudokuPuzzlesDontMatchFileException();
            }
        }
    }
}