using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AnnuaireEntreprise.Maui.Converter
{
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Ensure the value is a string
            if (value is string str)
            {
                // Return true if the string is NOT empty or whitespace
                return !string.IsNullOrWhiteSpace(str);
            }

            // Return false for non-string values
            return false;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
