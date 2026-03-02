using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CardTrackingVang.Converter
{
    // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/converters?view=net-maui-10.0
    // This class is needed instead of string format because I wish to bind it to a label.
    public class CurrencyConversion : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is decimal d) 
            {
                // Based on region of user.
                return d.ToString("C0", CultureInfo.CurrentCulture);
            } else
            {
                decimal t = 0;
                return t.ToString("C0", CultureInfo.CurrentCulture);
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is string s) 
            {
                return s.ToString();
            } else
            {
                return null;
            }
        }
    }
}
