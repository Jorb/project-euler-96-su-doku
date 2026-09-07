// <copyright file="SudokuPuzzleCell.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using SudokuSolver.Common.Const;

/// <summary>
/// An individual cell of a sudoku puzzle.
/// A cell contains a number between 1 and 9.
/// </summary>
internal class SudokuPuzzleCell
{
    /// <summary>
    /// 1-9 is valid. Null signifies empty.
    /// </summary>
    private int? initialValue = null;

    /// <summary>
    /// The current value entered in the cell.
    /// </summary>
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

    /// <summary>
    /// Gets the row that contains this cell.
    /// </summary>
    public SudokuPuzzleRow ParentRow { get; }

    /// <summary>
    /// Gets the column that contains this cell.
    /// </summary>
    public SudokuPuzzleColumn? ParentColumn { get; private set; }

    /// <summary>
    /// Gets the box that contains this cell.
    /// </summary>
    public SudokuPuzzleBox? ParentBox { get; private set; }

    /// <summary>
    /// Gets or sets the current value of the cell.
    /// </summary>
    public int? CurrentValue { get => this.currentValue; set => this.SetValue(value); }

    /// <summary>
    /// Gets a value indicating whether the initial value is null, then that means a value can be set.
    /// </summary>
    public bool IsSettable { get => this.initialValue is null; }

    /// <summary>
    /// If a value has been set in this cell.
    /// </summary>
    public bool IsSet { get => this.CurrentValue is not null; }

    /// <summary>
    /// Clear the cell value. Remove the number that was entered.
    /// </summary>
    public void ClearValue()
    {
        // Don't allow re-setting and firing changed event.
        if (this.IsSettable)
        {
            this.currentValue = null;
        }
    }

    /// <summary>
    /// Check if the cell was set and if it is valid.
    /// Valid means that the row, column and box of the puzzle are correct with no duplicate numbers.
    /// </summary>
    /// <returns>True if the cell has a value set (number from 1 to 9) and the puzzle is correct so far.</returns>
    internal bool IsSetAndValid()
    {
        if (this.ParentColumn is not null && this.ParentBox is not null)
        {
            // Check against the parent row, column and box for validity.
            return this.IsSet && this.ParentRow.IsValid && this.ParentColumn.IsValid && this.ParentBox.IsValid;
        }

        throw new CellParentsNotInitializedException();
    }

    /// <summary>
    /// Assign the parent column to this cell.
    /// </summary>
    /// <param name="parentColumn">The column that contains this cell.</param>
    /// <exception cref="CellParentColumnAlreadyAssignedException">Thrown if the parent column was assigned again.</exception>
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

    /// <summary>
    /// Assign the parent box to this cell.
    /// </summary>
    /// <param name="parentBox">The box that contains this cell.</param>
    /// <exception cref="CellParentBoxAlreadyAssignedException">Thrown if the parent box was assigned again.</exception>
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
            throw new AttemptedToSetUnSettableCellException();
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
            throw new InvalidSudokuCellValueException();
        }
    }
}