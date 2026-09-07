// <copyright file="CellParentColumnAlreadyAssignedException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown if a cell's parent column has already been set.
/// </summary>
[Serializable]
internal class CellParentColumnAlreadyAssignedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentColumnAlreadyAssignedException"/> class.
    /// </summary>
    public CellParentColumnAlreadyAssignedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentColumnAlreadyAssignedException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CellParentColumnAlreadyAssignedException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentColumnAlreadyAssignedException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CellParentColumnAlreadyAssignedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}