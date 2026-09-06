// <copyright file="SudokuPuzzleCell.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Console.Const;

internal class SudokuPuzzleCell
{
    /// <summary>
    /// 1-9 is valid. Null signifies empty.
    /// </summary>
    private int? initialValue = null;
    private int? currentValue = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="SudokuPuzzleCell"/> class.
    /// </summary>
    /// <param name="cellInt">The initial value of the cell. 0 is unassigned.</param>
    /// <param name="parentRow">The row that contains this cell.</param>
    public SudokuPuzzleCell(int cellInt, SudokuPuzzleRow parentRow)
    {
        this.ParentRow = parentRow;
        this.SetInitialValue(cellInt);
    }

    public SudokuPuzzleRow ParentRow { get; private set; }
    public SudokuPuzzleColumn ParentColumn { get; private set; }
    public SudokuPuzzleBox ParentBox { get; private set; }

    /// <summary>
    /// The current value of the cell.
    /// </summary>
    public int? CurrentValue { get => this.currentValue; set => this.SetValue(value); }

    /// <summary>
    /// If the initial value is null, then that means a value can be set.
    /// </summary>
    public bool IsSettable { get => this.initialValue is null; }

    /// <summary>
    /// If a value has been set in this cell.
    /// </summary>
    public bool IsSet { get => this.CurrentValue is not null; }

    public void ClearValue()
    {
        // Don't allow re-setting and firing changed event.
        if (this.IsSettable)
        {
            this.currentValue = null;
        }
    }

    internal bool IsSetAndValid()
    {
        // Check against the parent row, column and box for validity.
        return this.IsSet && this.ParentRow.IsValid && this.ParentColumn.IsValid && this.ParentBox.IsValid;
    }

    internal void AssignParentColumn(SudokuPuzzleColumn parentColumn)
    {
        if (this.ParentColumn is null)
        {
            this.ParentColumn = parentColumn;
        }
        else
        {
            throw new CellParentColumnAlreadyAssignedException();
        }
    }

    internal void AssignParentBox(SudokuPuzzleBox parentBox)
    {
        if (this.ParentBox is null)
        {
            this.ParentBox = parentBox;
        }
        else
        {
            throw new CellParentBoxAlreadyAssignedException();
        }
    }

    private void SetValue(int? value)
    {
        this.ThrowIfNotInitialized();

        if (this.IsSettable)
        {
            CheckValueValidity(value);

            this.currentValue = value;
        }
        else
        {
            // Be explicit, the solver should not be setting values it's not supposed to touch.
            throw new AttemptedToSetUnSettableRowException();
        }
    }

    /// <summary>
    /// Properly initialized cells must have all parents defined.
    /// </summary>
    private void ThrowIfNotInitialized()
    {
        if (this.ParentRow is null || this.ParentColumn is null || this.ParentBox is null)
        {
            throw new CellParentsNotInitializedException();
        }
    }

    private void SetInitialValue(int cellInt)
    {
        // 0 is not a valid number, but it is used by the file to signify empty.
        // Handle it early, use null from here on out.
        if (cellInt != 0)
        {
            CheckValueValidity(cellInt);
            this.initialValue = cellInt;
            this.currentValue = cellInt;
        }
    }

    private static void CheckValueValidity(int? cellValue)
    {
        // Don't allow nulls to be set after init. Call ClearValue if the cell needs to be cleared.
        if (cellValue is null || cellValue < SudokuConstants.MinValue || cellValue > SudokuConstants.MaxValue)
        {
            throw new InvalidSudokuCellValueException(cellValue);
        }
    }
}