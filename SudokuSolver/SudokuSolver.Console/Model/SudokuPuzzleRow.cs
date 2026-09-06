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
    /// Initializes a new instance of the <see cref="SudokuPuzzleRow"/> class.
    /// Build an instance of a row from a string.
    /// </summary>
    /// <param name="line">Line of integers read from file.</param>
    /// <exception cref="InvalidSudokuInitialCellCharException">Throw exception if the char cannot be parsed as an int between 1 and 9.</exception>
    public SudokuPuzzleRow(string line, int index)
    {
        this.Cells = new List<SudokuPuzzleCell>();
        this.Index = index;

        foreach (char cellValue in line.ToCharArray())
        {
            if (int.TryParse(cellValue.ToString(), out int cellInt))
            {
                var newCell = new SudokuPuzzleCell(cellInt, this);
                this.Cells.Add(newCell);
                //newCell.CellValueChanged += CellValueChanged;
            }
            else
            {
                throw new InvalidSudokuInitialCellCharException(cellValue);
            }
        }
    }
}