using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Console.Helper
{
    internal class PuzzleToStringListHelper
    {
        internal static List<string> BuildFileLinesFromPuzzleRows(int puzzleCount, SudokuPuzzle puzzle)
        {
            var fileLinesFromRows = new List<string>();
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
            return fileLinesFromRows;
        }

        internal static List<string> BuildFileLinesFromPuzzleColumns(int puzzleCount, SudokuPuzzle puzzle)
        {
            var fileLinesFromColumns = new List<string>();
            fileLinesFromColumns.Add($"Grid {puzzleCount.ToString("D2")}");
            for (int yIndex = 0; yIndex < 9; yIndex++)
            {
                string rowString = "";

                foreach (var column in puzzle.Columns)
                {

                    if (column.Cells[yIndex].CurrentValue is not null)
                    {
                        rowString += column.Cells[yIndex].CurrentValue.ToString();
                    }
                    else
                    {
                        rowString += "0";
                    }
                }
                fileLinesFromColumns.Add(rowString);

            }

            return fileLinesFromColumns;
        }
    }
}
