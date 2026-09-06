// <copyright file="CurrentCellSolveStateEnum.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

/// <summary>
/// States of the cell while solving.
/// </summary>
internal enum CurrentCellSolveStateEnum
{
    /// <summary>
    /// Nothing has happened, no known state.
    /// </summary>
    Unknown,

    /// <summary>
    /// Incremented past 9 and cell was cleared.
    /// </summary>
    Overflowed,

    /// <summary>
    /// Incremented the cell value by 1.
    /// </summary>
    Incremented,

    /// <summary>
    /// Set the cell value to 1.
    /// </summary>
    Initialized,
}