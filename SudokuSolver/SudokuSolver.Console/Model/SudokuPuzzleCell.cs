internal class SudokuPuzzleCell
{
    /// <summary>
    /// 1-9 is valid. Null signifies empty.
    /// </summary>
    private int? initialValue = null;
    private int? currentValue = null;

    private SudokuPuzzleRow parentRow = null;

    private SudokuPuzzleColumn parentColumn = null;
    private SudokuPuzzleBox parentBox = null;

    /// <summary>
    /// The current value of the cell.
    /// </summary>
    public int? CurrentValue { get => currentValue; set => SetValue(value); }

    /// <summary>
    /// If the initial value is null, then that means a value can be set.
    /// </summary>
    public bool IsSettable { get => initialValue is null; }

    /// <summary>
    /// If a value has been set in this cell.
    /// </summary>
    public bool IsSet { get => CurrentValue is not null; }

    public List<int> ValidNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public void ClearValue()
    {
        // Don't allow re-setting and firing changed event.
        if (IsSettable)
        {
            currentValue = null;
        }
    }

    private void SetValue(int? value)
    {
        if (IsSettable)
        {
            CheckValueValidity(value);

            CurrentValue = value;
        }
        else
        {
            // Be explicit, the solver should not be setting values it's not supposed to touch.
            throw new AttemptedToSetUnSettableRowException();
        }
    }

    //protected virtual void OnCellValueChanged(SudokuCellValueChangedEventArgs e)
    //{
    //    CellValueChanged?.Invoke(this, e);
    //}

    //public event EventHandler? CellValueChanged;

    public SudokuPuzzleCell(int cellInt, SudokuPuzzleRow sudokuPuzzleRow)
    {
        parentRow = sudokuPuzzleRow;
        SetInitialValue(cellInt);
    }

    private void SetInitialValue(int cellInt)
    {
        // 0 is not a valid number, but it is used by the file to signify empty.
        // Handle it early, use null from here on out.
        if (cellInt != 0)
        {
            CheckValueValidity(cellInt);
            initialValue = cellInt;
            currentValue = cellInt;
        }
    }

    private static void CheckValueValidity(int? cellInt)
    { 
        // Don't allow nulls to be set after init. Call ClearValue if the cell needs to be cleared.
        if (cellInt is null || cellInt < 1 || cellInt > 9)
        {
            throw new InvalidSudokuCellValueException(cellInt);
        }
    }

    internal bool IsSetAndValid()
    {
        // Check against the parent row, column and box for validity.
        return IsSet && parentRow.IsValid && parentColumn.IsValid && parentBox.IsValid;
    }

    internal void AssignParentColumn(SudokuPuzzleColumn parentColumn)
    {
        this.parentColumn = parentColumn;
    }
}