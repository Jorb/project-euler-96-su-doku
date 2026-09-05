[Serializable]
internal class CellParentColumnAlreadyAssignedException : Exception
{
    public CellParentColumnAlreadyAssignedException()
    {
    }

    public CellParentColumnAlreadyAssignedException(string? message) : base(message)
    {
    }

    public CellParentColumnAlreadyAssignedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}