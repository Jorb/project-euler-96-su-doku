// <copyright file="InvalidSudokuCellValueException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class InvalidSudokuCellValueException : Exception
{
    private readonly int cellInt;

    public InvalidSudokuCellValueException(int? cellInt)
    {
    }

    public InvalidSudokuCellValueException(string? message) : base(message)
    {
    }

    public InvalidSudokuCellValueException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}