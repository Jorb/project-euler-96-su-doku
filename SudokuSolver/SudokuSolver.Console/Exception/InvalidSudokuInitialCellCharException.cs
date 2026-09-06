// <copyright file="InvalidSudokuInitialCellCharException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class InvalidSudokuInitialCellCharException : Exception
{
    private readonly char cellValue;

    public InvalidSudokuInitialCellCharException()
    {
    }

    public InvalidSudokuInitialCellCharException(char cellValue)
    {
        this.cellValue = cellValue;
    }

    public InvalidSudokuInitialCellCharException(string? message) : base(message)
    {
    }

    public InvalidSudokuInitialCellCharException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}