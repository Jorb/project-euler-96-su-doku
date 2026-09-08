// <copyright file="InverseBooleanConverter.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

namespace SudokuSolver.Wpf.Converters
{
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Negates a boolean, so a control can be enabled while something is not happening.
    /// </summary>
    public sealed class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// A shared instance, so XAML can reference it with x:Static rather than declaring a resource.
        /// </summary>
        public static readonly InverseBooleanConverter Instance = new InverseBooleanConverter();

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is bool flag && !flag;

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is bool flag && !flag;
    }
}
