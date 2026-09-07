// <copyright file="CellParentBoxAlreadyAssignedException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown if attempting to re-assign a cell's parent box object.
/// </summary>
[Serializable]
internal class CellParentBoxAlreadyAssignedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentBoxAlreadyAssignedException"/> class.
    /// </summary>
    public CellParentBoxAlreadyAssignedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentBoxAlreadyAssignedException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CellParentBoxAlreadyAssignedException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CellParentBoxAlreadyAssignedException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CellParentBoxAlreadyAssignedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}