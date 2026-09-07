// <copyright file="OutsideOfPuzzleCellBoundsException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class OutsideOfPuzzleCellBoundsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OutsideOfPuzzleCellBoundsException"/> class.
    /// </summary>
    public OutsideOfPuzzleCellBoundsException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutsideOfPuzzleCellBoundsException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public OutsideOfPuzzleCellBoundsException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutsideOfPuzzleCellBoundsException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public OutsideOfPuzzleCellBoundsException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}