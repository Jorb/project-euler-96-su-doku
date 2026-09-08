// <copyright file="InvalidSudokuSolverSelected.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown if the user enters the wrong selection for the solver.
/// </summary>
[Serializable]
internal class InvalidSudokuSolverSelected : Exception
{
    private ConsoleKeyInfo solverKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuSolverSelected"/> class.
    /// </summary>
    public InvalidSudokuSolverSelected()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuSolverSelected"/> class.
    /// </summary>
    /// <param name="solverKey">THe invalid key entered.</param>
    public InvalidSudokuSolverSelected(ConsoleKeyInfo solverKey)
    {
        this.solverKey = solverKey;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuSolverSelected"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidSudokuSolverSelected(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidSudokuSolverSelected"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    public InvalidSudokuSolverSelected(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}