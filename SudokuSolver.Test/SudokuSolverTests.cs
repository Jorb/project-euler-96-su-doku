using SudokuSolver.Common.Helper;

namespace SudokuSolver.Test
{
    public class SudokuSolverTests
    {
        /// <summary>
        /// Build the sudoku definition file using each of the sudoku puzzle elements to prove it was built properly.
        /// </summary>
        [Fact]
        public void Verify_Puzzle_Object_Builds_Against_Definition_File()
        {
            var puzzleDefFilePath = "sudoku.txt";

            var puzzleFactory = new SudokuPuzzleFactory();
            var rawsudokuLines = File.ReadAllLines(puzzleDefFilePath);
            var puzzleList = puzzleFactory.BuildAllPuzzlesFromFile(puzzleDefFilePath);

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
                Assert.Equal(rawsudokuLines[i], fileLinesFromRows[i]);

                // Validate Column construction.
                Assert.Equal(rawsudokuLines[i], fileLinesFromColumns[i]);

                // Validate Box construction.
                Assert.Equal(rawsudokuLines[i], fileLinesFromBoxes[i]);
            }
        }
    }
}
