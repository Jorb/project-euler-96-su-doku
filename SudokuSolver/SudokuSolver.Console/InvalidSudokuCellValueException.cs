[Serializable]
internal class InvalidSudokuCellValueException : Exception
{
    private int cellInt;

    public InvalidSudokuCellValueException(int? cellInt)
    {
    }

    public InvalidSudokuCellValueException(string? message) : base(message)
    {
    }

    public InvalidSudokuCellValueException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}