using SudokuSolver.Console.Helper;

internal class SudokuCellCollection
{
    /// <summary>
    /// The cells that make up the row.
    /// </summary>
    public List<SudokuPuzzleCell> Cells;

    public bool IsComplete { get => CellListValidationHelper.AreCellsCompleted(Cells); }

    public bool IsValid { get => CellListValidationHelper.AreCellsValid(Cells); }

    public bool IsCompleteAndValid { get => IsComplete && IsValid; }
}