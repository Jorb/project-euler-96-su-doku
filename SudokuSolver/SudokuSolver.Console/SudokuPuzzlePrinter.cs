using SudokuSolver.Console.Helper;

internal class SudokuPuzzlePrinter
{
    public SudokuPuzzlePrinter()
    {
    }

    internal void PrintPuzzlesToFile(List<SudokuPuzzle> puzzleList, string outputPath)
    {
        var fileLines = new List<string>();
        var puzzleCount = 1;
        foreach (var puzzle in puzzleList)
        {
            //Convert the puzzle back into the file format using the rows objects
            fileLines.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzleCount, puzzle));

            puzzleCount++;
        }

        File.WriteAllLines(outputPath, fileLines);
    }
}