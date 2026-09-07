// <copyright file="CellParentsNotInitializedException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Cells parent objects (row, column, box) must be initialized before use.
/// </summary>
[Serializable]
internal class CellParentsNotInitializedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentsNotInitializedException"/> class.
    /// </summary>
    public CellParentsNotInitializedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentsNotInitializedException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CellParentsNotInitializedException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentsNotInitializedException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CellParentsNotInitializedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}