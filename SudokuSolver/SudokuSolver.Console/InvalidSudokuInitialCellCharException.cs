[Serializable]
internal class InvalidSudokuInitialCellCharException : Exception
{
    private char cellValue;

    public InvalidSudokuInitialCellCharException()
    {
    }

    public InvalidSudokuInitialCellCharException(char cellValue)
    {
        this.cellValue = cellValue;
    }

    public InvalidSudokuInitialCellCharException(string? message) : base(message)
    {
    }

    public InvalidSudokuInitialCellCharException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}