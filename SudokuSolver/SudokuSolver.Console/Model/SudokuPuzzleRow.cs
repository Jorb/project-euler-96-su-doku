// <copyright file="SudokuPuzzleRow.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

internal class SudokuPuzzleRow : SudokuCellCollection
{

    /// <summary>
    /// The index of the row in the puzzle.
    /// 0 is top row, 9 is bottom row.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// Build an instance of a row from a string.
    /// </summary>
    /// <param name="line">Line of integers read from file.</param>
    /// <exception cref="InvalidSudokuInitialCellCharException"></exception>
    public SudokuPuzzleRow(string line, int index)
    {
        Cells = new List<SudokuPuzzleCell>();
        Index = index;

        foreach (char cellValue in line.ToCharArray())
        {
            if (int.TryParse(cellValue.ToString(), out int cellInt))
            {
                var newCell = new SudokuPuzzleCell(cellInt, this);
                Cells.Add(newCell);
                //newCell.CellValueChanged += CellValueChanged;
            }
            else
            {
                throw new InvalidSudokuInitialCellCharException(cellValue);
            }
        }
    }
}