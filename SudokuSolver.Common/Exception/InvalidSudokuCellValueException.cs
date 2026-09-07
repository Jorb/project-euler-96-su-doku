// <copyright file="InvalidSudokuCellValueException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown when an invalid cell value is assigned to a cell.
/// </summary>
[Serializable]
internal class InvalidSudokuCellValueException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuCellValueException"/> class.
    /// </summary>
    public InvalidSudokuCellValueException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuCellValueException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidSudokuCellValueException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuCellValueException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidSudokuCellValueException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}