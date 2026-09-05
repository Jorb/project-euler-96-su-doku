using SudokuSolver.Console.Helper;

internal class SudokuPuzzleBox : SudokuCellCollection
{
    public SudokuPuzzleBox(List<SudokuPuzzleCell> cells, int positionX, int positionY)
    {
        Cells = new List<SudokuPuzzleCell>();
        foreach(var cell in cells)
        {
            cell.AssignParentBox(this);
            Cells.Add(cell);
        }
        PositionX = positionX;
        PositionY = positionY;
    }
    public int PositionX { get; }
    public int PositionY { get; }
}