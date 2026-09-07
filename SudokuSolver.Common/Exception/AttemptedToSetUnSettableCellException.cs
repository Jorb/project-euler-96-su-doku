// <copyright file="AttemptedToSetUnSettableRowException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

using System.Runtime.Serialization;

/// <summary>
/// Thrown if attempting to set a value in a cell that cannot be set (i.e. a value was in the cell in the puzzle definition.)
/// </summary>
[Serializable]
internal class AttemptedToSetUnSettableCellException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AttemptedToSetUnSettableCellException"/> class.
    /// </summary>
    public AttemptedToSetUnSettableCellException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AttemptedToSetUnSettableCellException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public AttemptedToSetUnSettableCellException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AttemptedToSetUnSettableCellException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public AttemptedToSetUnSettableCellException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}