// <copyright file="OutsideOfPuzzleCellBoundsException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class OutsideOfPuzzleCellBoundsException : Exception
{
    public OutsideOfPuzzleCellBoundsException()
    {
    }

    public OutsideOfPuzzleCellBoundsException(string? message) : base(message)
    {
    }

    public OutsideOfPuzzleCellBoundsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}