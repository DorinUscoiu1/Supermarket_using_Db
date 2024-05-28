using System;
using System.Globalization;
using System.Windows.Data;
using Supermarket.Model;

namespace Supermarket.Converters
{
    public class LoginConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                return new User()
                {
                    NumeUtilizator = values[0].ToString(),
                    Parola = values[1].ToString()
                };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
