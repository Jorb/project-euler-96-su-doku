[Serializable]
internal class CellParentsNotInitializedException : Exception
{
    public CellParentsNotInitializedException()
    {
    }

    public CellParentsNotInitializedException(string? message) : base(message)
    {
    }

    public CellParentsNotInitializedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}