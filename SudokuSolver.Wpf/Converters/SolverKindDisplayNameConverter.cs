// <copyright file="SolverKindDisplayNameConverter.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Converters
{
    using System.Globalization;
    using System.Windows.Data;
    using SudokuSolver.Wpf.Models;

    /// <summary>
    /// Renders a <see cref="SolverKind"/> using its friendly name.
    /// </summary>
    public sealed class SolverKindDisplayNameConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is SolverKind kind ? kind.ToDisplayName() : string.Empty;

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
