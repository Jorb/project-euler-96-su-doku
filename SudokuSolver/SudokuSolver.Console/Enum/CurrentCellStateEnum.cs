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