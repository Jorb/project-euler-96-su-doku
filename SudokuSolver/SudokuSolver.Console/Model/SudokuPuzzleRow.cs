using SudokuSolver.Console.Helper;
using System.Runtime.CompilerServices;

internal class SudokuPuzzleRow
{
    /// <summary>
    /// The cells that make up the row.
    /// </summary>
    public List<SudokuPuzzleCell> Cells;

    /// <summary>
    /// The Y position of the row.
    /// </summary>
    public int PositionY;

    /// <summary>
    /// Build an instance of a row from a string.
    /// </summary>
    /// <param name="line">Line of integers read from file.</param>
    /// <exception cref="InvalidSudokuInitialCellCharException"></exception>
    public SudokuPuzzleRow(string line, int positionY)
    {
        Cells = new List<SudokuPuzzleCell>();
        PositionY = positionY;

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

    public bool IsComplete { get => CellListValidationHelper.AreCellsCompleted(Cells); }
    public bool IsValid { get => CellListValidationHelper.AreCellsValid(Cells); }
    public bool IsCompleteAndValid { get => IsComplete && IsValid; }
}