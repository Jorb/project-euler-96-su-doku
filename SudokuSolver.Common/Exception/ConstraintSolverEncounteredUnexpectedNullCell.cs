// <copyright file="ConstraintSolverEncounteredUnexpectedNullCell.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown when an unexpected null value is encountered while solving with the constraint solver.
/// </summary>
[Serializable]
internal class ConstraintSolverEncounteredUnexpectedNullCell : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstraintSolverEncounteredUnexpectedNullCell"/> class.
    /// </summary>
    public ConstraintSolverEncounteredUnexpectedNullCell()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstraintSolverEncounteredUnexpectedNullCell"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public ConstraintSolverEncounteredUnexpectedNullCell(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConstraintSolverEncounteredUnexpectedNullCell"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ConstraintSolverEncounteredUnexpectedNullCell(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}