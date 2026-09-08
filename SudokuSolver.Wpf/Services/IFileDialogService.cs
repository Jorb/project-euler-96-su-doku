// <copyright file="IFileDialogService.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Services
{
    /// <summary>
    /// Wraps the shell file dialogs so view models stay free of direct UI calls.
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Asks the user where to save a file.
        /// </summary>
        /// <param name="suggestedFileName">The file name to pre-fill.</param>
        /// <returns>The chosen path, or null when the user cancelled.</returns>
        string? PromptForSavePath(string suggestedFileName);
    }
}
