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
        var puzzleCount = 1;
        foreach (var puzzle in puzzleList)
        {
            //Convert the puzzle back into the file format using the rows objects
            BuildFileLinesFromPuzzleRows(fileLinesFromRows, puzzleCount, puzzle);

            //Convert the puzzle back into the file format using the columns objects
            BuildFileLinesFromPuzzleColumns(fileLinesFromColumns, puzzleCount, puzzle);
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
        }

    }

    private static void BuildFileLinesFromPuzzleRows(List<string> fileLinesFromRows, int puzzleCount, SudokuPuzzle puzzle)
    {
        fileLinesFromRows.Add($"Grid {puzzleCount.ToString("D2")}");
        foreach (var row in puzzle.Rows)
        {
            string rowString = "";
            foreach (var cell in row.Cells)
            {
                if (cell.CurrentValue is not null)
                {
                    rowString += cell.CurrentValue.ToString();
                }
                else
                {
                    rowString += "0";
                }
            }
            fileLinesFromRows.Add(rowString);
        }
    }

    private static void BuildFileLinesFromPuzzleColumns(List<string> fileLinesFromColumns, int puzzleCount, SudokuPuzzle puzzle)
    {
        fileLinesFromColumns.Add($"Grid {puzzleCount.ToString("D2")}");
        for (int i = 0; i < 9; i++)
        {
            string rowString = "";

            foreach (var column in puzzle.Columns)
            {

                if (column.Cells[i].CurrentValue is not null)
                {
                    rowString += column.Cells[i].CurrentValue.ToString();
                }
                else
                {
                    rowString += "0";
                }
            }
            fileLinesFromColumns.Add(rowString);

        }
    }
}