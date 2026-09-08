// <copyright file="InvalidSudokuInitialCellCharException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown if a char from the sudoku puzzle definition file is invalid.
/// </summary>
[Serializable]
internal class InvalidSudokuInitialCellCharException : Exception
{
    private readonly char cellValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuInitialCellCharException"/> class.
    /// </summary>
    public InvalidSudokuInitialCellCharException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuInitialCellCharException"/> class.
    /// </summary>
    /// <param name="cellValue">The invalid char.</param>
    public InvalidSudokuInitialCellCharException(char cellValue)
    {
        this.cellValue = cellValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuInitialCellCharException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidSudokuInitialCellCharException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuInitialCellCharException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidSudokuInitialCellCharException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}