// <copyright file="InvalidSudokuSolverSelected.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// Thrown when the user selects an invalid solver by pressing the wrong key.
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
    /// <param name="solverKey">The key that was pressed to select the solver algorithm.</param>
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