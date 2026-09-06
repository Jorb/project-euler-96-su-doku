// <copyright file="CellParentBoxAlreadyAssignedException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class CellParentBoxAlreadyAssignedException : Exception
{
    public CellParentBoxAlreadyAssignedException()
    {
    }

    public CellParentBoxAlreadyAssignedException(string? message)
        : base(message)
    {
    }

    public CellParentBoxAlreadyAssignedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}