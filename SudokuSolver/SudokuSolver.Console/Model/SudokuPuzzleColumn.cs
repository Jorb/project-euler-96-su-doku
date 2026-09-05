using SudokuSolver.Console.Helper;

internal class SudokuPuzzleColumn : SudokuCellCollection
{
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
}