// <copyright file="PuzzleToStringListHelper.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Common.Helper
{
    using SudokuSolver.Common.Const;

    /// <summary>
    /// Convert a puzzle to a list of strings which match the definition text file.
    /// </summary>
    public class PuzzleToStringListHelper
    {
        /// <summary>
        /// Turn the puzzle into a list of strings with matches the puzzle definition file.
        /// </summary>
        /// <param name="puzzle">The puzzle to convert.</param>
        /// <returns>List of strings representing the puzzle object.</returns>
        public static List<string> BuildFileLinesFromPuzzleRows(SudokuPuzzle puzzle)
        {
            var fileLinesFromRows = new List<string>();
            fileLinesFromRows.Add(CreateGridTitle(puzzle.Id));
            foreach (var row in puzzle.Rows)
            {
                string rowString = string.Empty;
                foreach (var cell in row.Cells)
                {
                    rowString += ConvertCellToFileStringValue(cell);
                }

                fileLinesFromRows.Add(rowString);
            }

            return fileLinesFromRows;
        }

        /// <summary>
        /// Use the column objects to build the file.
        /// This is validation that the columns were constructed properly.
        /// </summary>
        /// <param name="puzzle">Sudoku puzzle in-memory object.</param>
        /// <returns>List of strings matching the format of the sudoku definition file.</returns>
        public static List<string> BuildFileLinesFromPuzzleColumns(SudokuPuzzle puzzle)
        {
            var fileLinesFromColumns = new List<string>();
            fileLinesFromColumns.Add(CreateGridTitle(puzzle.Id));
            for (int yIndex = 0; yIndex < SudokuConstants.MaxValue; yIndex++)
            {
                string rowString = string.Empty;

                foreach (var column in puzzle.Columns)
                {
                    rowString += ConvertCellToFileStringValue(column.Cells[yIndex]);
                }

                fileLinesFromColumns.Add(rowString);
            }

            return fileLinesFromColumns;
        }

        /// <summary>
        /// Use the box objects to build the file.
        /// This is validation that the boxes were constructed properly.
        /// </summary>
        /// <param name="puzzle">Sudoku puzzle in-memory object.</param>
        /// <returns>List of strings matching the format of the sudoku definition file.</returns>
        public static List<string> BuildFileLinesFromPuzzleBoxes(SudokuPuzzle puzzle)
        {
            var fileLinesFromBoxes = new List<string>();
            fileLinesFromBoxes.Add(CreateGridTitle(puzzle.Id));

            for (int puzzleRow = 0; puzzleRow < SudokuConstants.MaxValue; puzzleRow++)
            {
                string newFileLine = string.Empty;

                // Iterate the columns and get the associated box value.
                for (int puzzleColumn = 0; puzzleColumn < SudokuConstants.MaxValue; puzzleColumn += SudokuConstants.BoxInnerColumns)
                {
                    var currentBox = puzzle.GetBoxFromRawCellCoordinates(puzzleRow, puzzleColumn);

                    var innerBoxRow = puzzleRow % SudokuConstants.BoxColumnsPerPuzzle;

                    // Boxes have 3 rows inside. Get the inner row.
                    var currentBoxRowValues = currentBox.GetInnerRowCells(innerBoxRow);
                    foreach (var cell in currentBoxRowValues)
                    {
                        newFileLine += ConvertCellToFileStringValue(cell);
                    }
                }

                fileLinesFromBoxes.Add(newFileLine);
            }

            return fileLinesFromBoxes;
        }

        private static string CreateGridTitle(int puzzleNumber)
        {
            return $"Grid {puzzleNumber.ToString("D2")}";
        }

        /// <summary>
        /// Converts a cell to it's string value for storing in the file.
        /// </summary>
        /// <param name="cell">The cell to get a value from.</param>
        /// <returns>The cell value as a string.</returns>
        private static string ConvertCellToFileStringValue(SudokuPuzzleCell cell)
        {
            if (cell.CurrentValue is not null)
            {
                return cell.CurrentValue.ToString();
            }
            else
            {
                // The file uses 0 to signify unassigned values cells. My logic uses null.
                return SudokuConstants.FileEmptyCell;
            }
        }
    }
}
