// <copyright file="Program.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class InvalidSudokuSolverSelected : Exception
{
    private ConsoleKeyInfo solverKey;

    public InvalidSudokuSolverSelected()
    {
    }

    public InvalidSudokuSolverSelected(ConsoleKeyInfo solverKey)
    {
        this.solverKey = solverKey;
    }

    public InvalidSudokuSolverSelected(string? message)
        : base(message)
    {
    }

    public InvalidSudokuSolverSelected(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}