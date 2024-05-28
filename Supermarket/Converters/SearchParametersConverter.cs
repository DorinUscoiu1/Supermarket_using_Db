using Supermarket.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Supermarket.Converters
{
    public class SearchParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new SearchParameters
            {
                Nume = values[0]?.ToString(),
                CodBare = values[1]?.ToString(),
                DataExpirare = values[2] as DateTime?,
                ProducatorId = values[3] as int?,
                CategorieId = values[4] as int?
            };
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
