using Supermarket.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Supermarket.Converters
{
    public class ProducatorConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                return new Producator
                {
                    Nume = values[0]?.ToString(),
                    TaraOrigine = values[1]?.ToString()
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
