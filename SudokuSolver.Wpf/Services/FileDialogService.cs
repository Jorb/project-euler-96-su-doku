// <copyright file="FileDialogService.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    using Microsoft.Win32;

    /// <summary>
    /// The real shell file dialogs.
    /// </summary>
    public sealed class FileDialogService : IFileDialogService
    {
        /// <inheritdoc/>
        public string? PromptForSavePath(string suggestedFileName)
        {
            var dialog = new SaveFileDialog
            {
                FileName = suggestedFileName,
                DefaultExt = ".txt",
                Filter = "Sudoku definition files (*.txt)|*.txt|All files (*.*)|*.*",
                AddExtension = true,
                OverwritePrompt = true,
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}
