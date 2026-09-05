using SudokuSolver.Console.Helper;

internal class SudokuPuzzleFactory : ISudokuPuzzleFactory
{
    private string sudokuFilePath;

    public SudokuPuzzleFactory()
    {
    }

    public SudokuPuzzleFactory(string sudokuFilePath)
    {
        this.sudokuFilePath = sudokuFilePath;
    }

    private List<SudokuPuzzle> BuildAllPuzzlesFromFile(string sudokuFilePath)
    {
        List<SudokuPuzzle> sudokuPuzzleList = new List<SudokuPuzzle>();

        var rawsudokuLines = File.ReadAllLines(sudokuFilePath);

        //Parse out each puzzle (could do this with fancy regex, but I don't wanna right now.)
        for (int i = 0; i < rawsudokuLines.Length; i += 10)
        {
            List<string> puzzleLines = new List<string>();
            for (int j = i + 1; j < i + 10; j++)
            {
                puzzleLines.Add(rawsudokuLines[j]);
            }
            sudokuPuzzleList.Add(new SudokuPuzzle(puzzleLines));
        }
        ValidatePuzzlesAgainstFile(sudokuPuzzleList, rawsudokuLines);
        return sudokuPuzzleList;
    }

    public List<SudokuPuzzle> BuildPuzzleList()
    {
        return BuildAllPuzzlesFromFile(this.sudokuFilePath);
    }

    void ValidatePuzzlesAgainstFile(List<SudokuPuzzle> puzzleList, string[] rawsudokuLines)
    {
        List<string> fileLinesFromRows = new List<string>();
        List<string> fileLinesFromColumns = new List<string>();
        List<string> fileLinesFromBoxes = new List<string>();

        var puzzleCount = 1;
        foreach (var puzzle in puzzleList)
        {
            //Convert the puzzle back into the file format using the rows objects
            fileLinesFromRows.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleRows(puzzleCount, puzzle));

            //Convert the puzzle back into the file format using the columns objects
            fileLinesFromColumns.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleColumns(puzzleCount, puzzle));

            //Convert the puzzle back into the file format using the box objects
            fileLinesFromBoxes.AddRange(PuzzleToStringListHelper.BuildFileLinesFromPuzzleBoxes(puzzleCount, puzzle));

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

    //private static void BuildFileLinesFromPuzzleRows(List<string> fileLinesFromRows, int puzzleCount, SudokuPuzzle puzzle)
    //{
    //    fileLinesFromRows.Add($"Grid {puzzleCount.ToString("D2")}");
    //    foreach (var row in puzzle.Rows)
    //    {
    //        string rowString = "";
    //        foreach (var cell in row.Cells)
    //        {
    //            if (cell.CurrentValue is not null)
    //            {
    //                rowString += cell.CurrentValue.ToString();
    //            }
    //            else
    //            {
    //                rowString += "0";
    //            }
    //        }
    //        fileLinesFromRows.Add(rowString);
    //    }
    //}

    //private static void BuildFileLinesFromPuzzleColumns(List<string> fileLinesFromColumns, int puzzleCount, SudokuPuzzle puzzle)
    //{
    //    fileLinesFromColumns.Add($"Grid {puzzleCount.ToString("D2")}");
    //    for (int yIndex = 0; yIndex < 9; yIndex++)
    //    {
    //        string rowString = "";

    //        foreach (var column in puzzle.Columns)
    //        {

    //            if (column.Cells[yIndex].CurrentValue is not null)
    //            {
    //                rowString += column.Cells[yIndex].CurrentValue.ToString();
    //            }
    //            else
    //            {
    //                rowString += "0";
    //            }
    //        }
    //        fileLinesFromColumns.Add(rowString);

    //    }
    //}


    private static void BuildFileLinesFromPuzzleBoxes(List<string> fileLinesFromBoxes, int puzzleCount, SudokuPuzzle puzzle)
    {
        //fileLinesFromBoxes.Add($"Grid {puzzleCount.ToString("D2")}");
        //int boxXIndex = 0;
        //int boxYIndex = 0;
        //foreach(var box in puzzle.Boxes)
        //{
        //    string boxRow0, boxRow1, boxRow2;

        //    boxRow0 = ${ puzzle.Boxes[0].Cells }

        //    boxXIndex++;
        //    boxXIndex = boxXIndex % 3;
        //    if(boxXIndex == 0)
        //    {
        //        boxYIndex++;
        //    }
        //}



        //for (int yIndexBox = 0; yIndexBox < 3; yIndexBox++)
        //{
        //    string rowString = "";

        //    for (int j = yIndexBox; j < yIndexBox + 3; j++)
        //    {
        //        var box = puzzle.Boxes[j];

        //        for (int k = 0; k < 3; k++)
        //        {
        //            if (box.Cells[k].CurrentValue is not null)
        //            {
        //                rowString += column.Cells[yIndexBox].CurrentValue.ToString();
        //            }
        //            else
        //            {
        //                rowString += "0";
        //            }
        //        }
        //    }
        //    fileLinesFromBoxes.Add(rowString);

        //}
    }
}