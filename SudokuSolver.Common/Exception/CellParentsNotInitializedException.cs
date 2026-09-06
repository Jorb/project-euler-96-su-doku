// <copyright file="CellParentsNotInitializedException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class CellParentsNotInitializedException : Exception
{
    public CellParentsNotInitializedException()
    {
    }

    public CellParentsNotInitializedException(string? message)
        : base(message)
    {
    }

    public CellParentsNotInitializedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}