// <copyright file="CurrentCellStateEnum.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

internal enum CurrentCellStateEnum
{
    Unknown,
    /// <summary>
    /// Incremented past 9 and was cleared.
    /// </summary>
    Overflowed,
    Incremented,
    Initialized
}