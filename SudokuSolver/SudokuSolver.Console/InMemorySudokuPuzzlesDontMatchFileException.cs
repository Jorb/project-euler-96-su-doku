[Serializable]
internal class InMemorySudokuPuzzlesDontMatchFileException : Exception
{
    public InMemorySudokuPuzzlesDontMatchFileException()
    {
    }

    public InMemorySudokuPuzzlesDontMatchFileException(string? message) : base(message)
    {
    }

    public InMemorySudokuPuzzlesDontMatchFileException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}