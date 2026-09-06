// <copyright file="SudokuPuzzlePrinter.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Helper;

public class SudokuPuzzlePrinter
{
    public SudokuPuzzlePrinter()
    {
    }

    public void PrintPuzzlesToFile(List<SudokuPuzzle> puzzleList, string outputPath)
    {
        var fileLines = new List<string>();
        foreach (var puzzle in puzzleList)
        {
            //Convert the puzzle back into the file format using the rows objects
            fileLines.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzle));
        }

        File.WriteAllLines(outputPath, fileLines);
    }
}