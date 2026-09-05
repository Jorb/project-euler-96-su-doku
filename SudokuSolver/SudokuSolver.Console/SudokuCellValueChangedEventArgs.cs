internal class SudokuCellValueChangedEventArgs : EventArgs
{
    SudokuCellValueChangedEventArgs(SudokuPuzzleCell sender)
    {
        Sender = sender;
    }

    public SudokuPuzzleCell Sender { get; set;  }
}