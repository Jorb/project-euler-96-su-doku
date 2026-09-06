// <copyright file="InMemorySudokuPuzzlesDontMatchFileException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class InMemorySudokuPuzzlesDontMatchFileException : Exception
{
    public InMemorySudokuPuzzlesDontMatchFileException()
    {
    }

    public InMemorySudokuPuzzlesDontMatchFileException(string? message)
        : base(message)
    {
    }

    public InMemorySudokuPuzzlesDontMatchFileException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}