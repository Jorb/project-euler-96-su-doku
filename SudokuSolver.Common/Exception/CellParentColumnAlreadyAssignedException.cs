// <copyright file="CellParentColumnAlreadyAssignedException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class CellParentColumnAlreadyAssignedException : Exception
{
    public CellParentColumnAlreadyAssignedException()
    {
    }

    public CellParentColumnAlreadyAssignedException(string? message)
        : base(message)
    {
    }

    public CellParentColumnAlreadyAssignedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}