using SudokuSolver.Console;

internal class SudokuPuzzleColumn
{
    public List<SudokuPuzzleCell> Cells;

    public int PositionX { get; }

    public SudokuPuzzleColumn(List<SudokuPuzzleCell> cells, int positionX)
    {
        Cells = new List<SudokuPuzzleCell>();
        this.PositionX = positionX;
        foreach (var cell in cells)
        {
            cell.AssignParentColumn(this);
            Cells.Add(cell);
        }
    }

    public bool IsValid { get => GetIsValid(); }

    private bool GetIsValid()
    {
        return CellListValidationHelper.AreCellsValid(Cells);
    }
}